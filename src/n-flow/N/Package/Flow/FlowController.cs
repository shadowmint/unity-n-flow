using System;
using N.Package.Flow.Dispatchers;
using N.Package.Flow.Infrastructure;
using UnityEngine;

namespace N.Package.Flow
{
  
  public abstract class FlowController<TState> : FlowComponentBase where TState : FlowComponentState
  {
    /// <summary>
    /// The FlowComponentState for this component tree.
    /// </summary>
    public TState State;

    /// <summary>
    /// The tree of components for this controller.
    /// For debugging only; use 'Debug' to update this.
    /// </summary>
    [TextArea] public string Debug;

    [NonSerialized] public FlowComponentDebugHeirarchy DebugData;

    public FlowControllerActions Actions = new FlowControllerActions();

    private IFlowDispatcher _dispatcher;

    private FlowVirtualComponentHeirarchy _componentHeirarchy;

    private FlowPrefabFactory _factory;

    protected abstract IFlowComponent OnComponentLayout();

    public void SetState(TState state)
    {
      State = state;
      Actions.Update = true;
    }

    public void SetDispatcher(IFlowDispatcher dispatcher)
    {
      _dispatcher = dispatcher;
    }

    public FlowVirtualComponentHeirarchy GetComponentHeirarchy()
    {
      return GetOrCreateComponentHeirarchy();
    }

    public void Update()
    {
      RequireInitialization();
      var rootComponent = OnComponentLayout();
      MountOrUnmountComponentTree(rootComponent);
      AutoUpdateComponentsFromState(rootComponent);
      RebuildDebugHeirarchy();
    }

    private void RequireInitialization()
    {
      if (_dispatcher != null) return;
      _factory = new FlowPrefabFactory(this);
      _dispatcher = new FlowDispatcher(_factory);
    }

    private void RebuildDebugHeirarchy()
    {
      if (!Actions.Debug) return;
      if (_componentHeirarchy == null) return;
      DebugData = _componentHeirarchy.GenerateDebugHeirarchy();
      Debug = DebugData.ToString();
      Actions.Debug = false;
    }

    private void AutoUpdateComponentsFromState(IFlowComponent rootComponent)
    {
      if (!Actions.Update) return;
      if (_componentHeirarchy == null) return;
      Actions.Update = false;
      _componentHeirarchy.Update(rootComponent);
    }

    private void MountOrUnmountComponentTree(IFlowComponent rootComponent)
    {
      // Returning null is the equivalent of setting mount to false.
      if (rootComponent != null && Actions.Mount)
      {
        if (_componentHeirarchy == null)
        {
          GetOrCreateComponentHeirarchy();
        }
      }
      else
      {
        if (_componentHeirarchy == null) return;
        _componentHeirarchy.Dispose();
        _componentHeirarchy = null;
      }
    }

    private FlowVirtualComponentHeirarchy GetOrCreateComponentHeirarchy()
    {
      return _componentHeirarchy ?? (_componentHeirarchy = new FlowVirtualComponentHeirarchy(this, _dispatcher));
    }
  }
}