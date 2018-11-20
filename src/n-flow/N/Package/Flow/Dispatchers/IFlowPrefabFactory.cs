using UnityEngine;

namespace N.Package.Flow.Dispatchers
{
  public interface IFlowPrefabFactory
  {
    GameObject Load(IFlowComponent virtualComponentComponent);
  }
}