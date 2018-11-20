using N.Package.Flow.Dispatchers;
using N.Package.Flow.Infrastructure;
using UnityEngine;

namespace N.Package.Flow
{
  public interface IFlowDispatcher
  {
    void DestroyComponentInstance(FlowVirtualComponent virtualComponent);
    void CreateComponentInstance(FlowVirtualComponent flowVirtualComponent, IFlowComponentBaseProvider baseProvider);
    FlowComponentProperties GetProperties(FlowVirtualComponent virtualComponent);
  }
}