using System;
using UnityEngine;

namespace N.Package.Flow
{
  public abstract class FlowComponentState
  {
    /// <summary>
    /// Used to generate a unique reference from the component parent to the child instance.
    /// For most purposes, the default implementation is sufficient for unique state types.
    /// For loops, you should override this and return identity from the state object.
    /// </summary>
    public virtual string Identity => DefaultIdentity();

    /// <summary>
    /// A deferred function to be invoked after the parent component is ready to
    /// resolve the container element from the parent component's props.
    /// </summary>
    public Func<GameObject> Container { get; set; }

    /// <summary>
    /// Marks if the component was updated last render or not.
    /// </summary>
    public bool Updated { get; set; }

    private string DefaultIdentity()
    {
      return GetType().FullName;
    }
  }
}