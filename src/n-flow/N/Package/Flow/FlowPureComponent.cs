using System;
using System.Collections.Generic;

namespace N.Package.Flow
{
  /// <summary>
  /// A state only component that never renders its own UI.
  /// This is for 'logical' components.
  /// </summary>
  /// <typeparam name="TState"></typeparam>
  public abstract class FlowPureComponent<TState> : IFlowComponent where TState : FlowComponentState 
  {
    FlowComponentState IFlowComponent.State => State;

    /// <summary>
    /// The FlowComponentState for this component
    /// </summary>
    public TState State { get; }

    /// <summary>
    /// Return the concrete properties type.
    /// </summary>
    public Type PropertiesType => null;

    protected FlowPureComponent(TState state)
    {
      State = state;
    }

    /// <summary>
    /// Override this to control the default behaviour, which is that components always render. 
    /// </summary>
    public virtual bool ComponentShouldUpdate()
    {
      return true;
    }

    public void OnComponentDidMount(FlowComponentProperties props)
    {
    }

    public void OnComponentWillUnmount()
    {
      
    }

    /// <summary>
    /// Triggered immediately before OnComponentLayout for a component.
    /// </summary>
    public abstract void OnComponentRender();

    /// <summary>
    /// Triggered to redarw a component.
    /// </summary>
    public abstract IEnumerable<IFlowComponent> OnComponentLayout();
  }
}