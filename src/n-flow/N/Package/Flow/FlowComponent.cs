using System;
using System.Collections.Generic;
using UnityEngine;

namespace N.Package.Flow
{
  public abstract class FlowComponent<TState, TProps> : IFlowComponent
    where TState : FlowComponentState where TProps : FlowComponentProperties
  {
    FlowComponentState IFlowComponent.State => State;

    /// <summary>
    /// The FlowComponentState for this component
    /// </summary>
    public TState State { get; }

    /// <summary>
    /// The properties for this component.
    /// </summary>
    protected TProps Properties { get; set; }

    /// <summary>
    /// Return the concrete properties type.
    /// </summary>
    public Type PropertiesType => typeof(TProps);

    protected FlowComponent(TState state)
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

    public virtual void OnComponentDidMount(FlowComponentProperties props, bool justMounted)
    {
      Properties = props as TProps;
      if (Properties == null)
      {
        throw new Exception($"Invalid prefab for component; missing {typeof(TProps)}");
      }
    }

    public virtual void OnComponentWillUnmount()
    {
      Properties = null;
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