using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace N.Package.Flow
{
  public class FlowVirtualComponent
  {
    public int MaxComponentHeirarchyDepth { get; set; } = 10;

    public bool IsUsed { get; set; }

    public GameObject Instance { get; set; }

    private IFlowComponent _component;
    private readonly IFlowDispatcher _dispatcher;

    private readonly IList<FlowVirtualComponent> _children = new List<FlowVirtualComponent>();
    private bool _manifest;

    public string Prefab => _component?.ComponentResourcePath();

    public IFlowComponent Component => _component;

    public FlowVirtualComponent(IFlowDispatcher dispatcher)
    {
      _dispatcher = dispatcher;
    }

    public void Update(IFlowComponent component, FlowVirtualComponent parent, int depth = 0)
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
        _dispatcher?.CreateComponentInstance(this);
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
        _component.OnComponentRender();
        foreach (var component in _component.OnComponentLayout())
        {
          var child = FindOrCreateVirtualComponentFor(component);
          child.Update(component, this, depth + 1);
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
      if (matches.Length == 0)
      {
        var instance = new FlowVirtualComponent(_dispatcher);
        _children.Add(instance);
        return instance;
      }

      if (matches.Length == 1)
      {
        return matches[0];
      }

      throw new Exception($"Duplicate identity {component.State.Identity} in {_component.State.Identity}. For loops, explicitly set the identity value of duplicate element state.");
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
  }
}