using System;
using System.Collections.Generic;
using System.Linq;
using N.Package.Flow.Infrastructure;
using UnityEngine;

namespace N.Package.Flow
{
  public class FlowVirtualComponent : IFlowComponentBaseProvider
  {
    public int MaxComponentHeirarchyDepth { get; set; } = 25;

    public bool IsUsed { get; set; }

    public GameObject Instance { get; set; }

    private IFlowComponent _component;

    private readonly IFlowComponentBaseProvider _parentProvider;

    private readonly IFlowDispatcher _dispatcher;

    private readonly IList<FlowVirtualComponent> _children = new List<FlowVirtualComponent>();

    private bool _manifest;

    public IFlowComponent Component => _component;

    public Type PropertiesType => Component?.PropertiesType;

    public FlowVirtualComponent(IFlowComponentBaseProvider parentProvider, IFlowDispatcher dispatcher)
    {
      _parentProvider = parentProvider;
      _dispatcher = dispatcher;
    }

    public void Update(IFlowComponent component, int depth = 0)
    {
      _component = component;

      RequireManifestInstance();

      if (_component.ComponentShouldUpdate())
      {
        RenderComponentLayout(depth);
        _component.State.Updated = true;
      }
      else
      {
        _component.State.Updated = false;
      }
    }

    private void RequireManifestInstance()
    {
      if (!_manifest)
      {
        _manifest = true;
        _dispatcher?.CreateComponentInstance(this, _parentProvider);
      }

      var props = _dispatcher?.GetProperties(this);
      _component.OnComponentDidMount(props);
    }

    private void RenderComponentLayout(int depth)
    {
      if (depth > MaxComponentHeirarchyDepth)
      {
        throw new Exception("Component heirarchy too deeply nested. Possible recursive loop?");
      }

      foreach (var child in _children)
      {
        child.IsUsed = false;
      }

      try
      {
        // Render component properties
        _component.OnComponentRender();

        // All children must be unique
        var newChildren = _component.OnComponentLayout().ToList();
        if (newChildren.Select(i => i.State.Identity).Distinct().Count() != newChildren.Count)
        {
          var identityList = string.Join(", ", newChildren.Select(i => i.State.Identity));
          throw new Exception(
            $"Duplicate identities in child list: {identityList} in {_component.State.Identity}. For loops, explicitly set the identity value of duplicate element state.");
        }

        // Render all child components
        foreach (var component in newChildren)
        {
          var child = FindOrCreateVirtualComponentFor(component);
          child.Update(component, depth + 1);
          child.IsUsed = true;
        }
      }
      catch (Exception err)
      {
        Debug.LogError($"Error rendering component {_component.GetType()} : {Component.State.Identity}");
        Debug.LogException(err);
      }

      foreach (var child in _children.Where(i => !i.IsUsed).ToList())
      {
        _children.Remove(child);
        DisposeVirtualComponent(child);
      }
    }

    private FlowVirtualComponent FindOrCreateVirtualComponentFor(IFlowComponent component)
    {
      var matches = _children.Where(i => i.Component.State.Identity.Equals(component.State.Identity)).ToArray();
      if (matches.Length > 0)
      {
        var match = matches[0];
        if (match.PropertiesType == component.PropertiesType)
        {
          return match;
        }
      }

      var instance = new FlowVirtualComponent(this, _dispatcher);
      _children.Add(instance);
      return instance;
    }

    private void DisposeVirtualComponent(FlowVirtualComponent child)
    {
      child?.Component?.OnComponentWillUnmount();
      _dispatcher?.DestroyComponentInstance(child);
    }

    public FlowComponentDebugHeirarchy GenerateDebugHeirarchy()
    {
      return new FlowComponentDebugHeirarchy()
      {
        Identity = Component.State.Identity,
        Children = _children.Select(i => i.GenerateDebugHeirarchy()).ToList(),
        Updated = _component.State.Updated
      };
    }

    public void Unmount()
    {
      foreach (var child in _children)
      {
        child.Unmount();
      }

      DisposeVirtualComponent(this);
    }

    public FlowComponentBase ComponentBase(IFlowDispatcher dispatcher)
    {
      return dispatcher.GetProperties(this);
    }

    public IFlowComponentBaseProvider Parent => _parentProvider;
  }
}