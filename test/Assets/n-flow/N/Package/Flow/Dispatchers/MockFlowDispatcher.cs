using UnityEngine;

namespace N.Package.Flow.Dispatchers
{
  public class MockFlowDispatcher : IFlowDispatcher
  {
    public void DestroyComponentInstance(FlowVirtualComponent virtualComponent)
    {
      if (virtualComponent.Instance == null) return;
      Object.DestroyImmediate(virtualComponent.Instance);
    }

    public void CreateComponentInstance(FlowVirtualComponent flowVirtualComponent)
    {
      var instance = new GameObject();
      flowVirtualComponent.Instance = instance;
    }

    public FlowComponentProperties GetProperties(FlowVirtualComponent virtualComponent)
    {
      var propType = virtualComponent.Component?.PropertiesType;
      var instance = virtualComponent.Instance?.GetComponent(propType) as FlowComponentProperties;
      if (instance != null) return instance;
      virtualComponent.Instance?.AddComponent(propType);
      instance = virtualComponent.Instance?.GetComponent(propType) as FlowComponentProperties;
      return instance;
    }
  }
}