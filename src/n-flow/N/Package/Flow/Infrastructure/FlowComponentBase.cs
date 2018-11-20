using N.Package.Flow.Dispatchers;
using UnityEngine;

namespace N.Package.Flow.Infrastructure
{
  [RequireComponent(typeof(FlowControllerComponentHelper))]
  public class FlowComponentBase : MonoBehaviour, IFlowComponentBaseProvider
  {
    [Tooltip("The set of components available in this controller")]
    public FlowComponentProperties[] Components;
    
    private IFlowPrefabFactory _componentFactory;
    
    /// <summary>
    /// This allows component base types to manufacture the prefabs for this child elements.
    /// </summary>
    public IFlowPrefabFactory ComponentFactory => _componentFactory ?? (_componentFactory = new FlowPrefabFactory(this));

    public FlowComponentBase ComponentBase(IFlowDispatcher dispatcher)
    {
      return this;
    }

    public IFlowComponentBaseProvider Parent => null;
  }
}