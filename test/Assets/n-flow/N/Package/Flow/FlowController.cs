using System;
using N.Package.Flow.Dispatchers;
using UnityEngine;

namespace N.Package.Flow
{
  public abstract class FlowController<TState> : MonoBehaviour where TState : FlowComponentState
  {
    /// <summary>
    /// The FlowComponentState for this component tree.
    /// </summary>
    public TState State;

    /// <summary>
    /// The tree of components for this controller.
    /// For debugging only; use 'Debug' to update this.
    /// </summary>
    [TextArea]
    public string Debug;

    [NonSerialized]
    public FlowComponentDebugHeirarchy DebugData;

    public FlowControllerActions Actions = new FlowControllerActions();

    private IFlowDispatcher _dispatcher = new FlowDispatcher();
    private FlowVirtualComponentHeirarchy _componentHeirarchy;

    protected abstract IFlowComponent OnComponentLayout();

    public void SetState(TState state)
    {
      State = state;
      GetOrCreateComponentHeirarchy().Update(OnComponentLayout());
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
      MountOrUnmountComponentTree();
      AutoUpdateComponentsFromState();
      RebuildDebugHeirarchy();
    }

    private void RebuildDebugHeirarchy()
    {
      if (!Actions.Debug) return;
      if (_componentHeirarchy == null) return;
      DebugData = _componentHeirarchy.GenerateDebugHeirarchy();
      Debug = DebugData.ToString();
      Actions.Debug = false;
    }

    private void AutoUpdateComponentsFromState()
    {
      if (!Actions.Update) return;
      if (_componentHeirarchy == null) return;
      Actions.Update = false;
      _componentHeirarchy.Update(OnComponentLayout());
    }

    private void MountOrUnmountComponentTree()
    {
      if (Actions.Mount)
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
      OnComponentLayout();
      return _componentHeirarchy ?? (_componentHeirarchy = new FlowVirtualComponentHeirarchy(_dispatcher));
    }
  }
}