using UnityEngine;

namespace N.Package.Flow
{
  public interface IFlowDispatcher
  {
    void DestroyComponentInstance(FlowVirtualComponent virtualComponent);
    void CreateComponentInstance(FlowVirtualComponent flowVirtualComponent);
    FlowComponentProperties GetProperties(FlowVirtualComponent virtualComponent);
  }
}