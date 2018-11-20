using System;
using UnityEngine;

namespace N.Package.Flow.Dispatchers
{
  public class FlowDispatcher : IFlowDispatcher
  {
    public void DestroyComponentInstance(FlowVirtualComponent virtualComponent)
    {
      if (virtualComponent.Instance == null) return;
      UnityEngine.Object.Destroy(virtualComponent.Instance);
      virtualComponent.Instance = null;
    }

    public void CreateComponentInstance(FlowVirtualComponent virtualComponent)
    {
      if (virtualComponent.Instance != null) return;

      // If the parent is NULL, don't spawn a component instance at all.
      var parent = virtualComponent.Component?.State.Container?.Invoke();
      if (parent == null)
      {
        if (virtualComponent.Prefab != null)
        {
          throw new Exception($"Component {virtualComponent.Component} with template {virtualComponent.Prefab} had no Container to spawn into!");
        }

        virtualComponent.Instance = null;
        return;
      }

      var template = virtualComponent.Prefab;
      var prefab = template == null ? null : LoadPrefabFor(template);
      virtualComponent.Instance = SpawnInstance(prefab, parent);
    }

    public FlowComponentProperties GetProperties(FlowVirtualComponent virtualComponent)
    {
      var propType = virtualComponent.Component?.PropertiesType;
      return virtualComponent.Instance?.GetComponent(propType) as FlowComponentProperties;
    }

    private GameObject SpawnInstance(GameObject prefab, GameObject parent)
    {
      try
      {
        var instance = prefab == null ? new GameObject() : UnityEngine.Object.Instantiate(prefab);
        if (parent == null) return instance;

        instance.transform.SetParent(parent.transform, false);
        return instance;
      }
      catch (Exception e)
      {
        throw new Exception($"Failed to spawn prefab: {e}");
      }
    }

    private GameObject LoadPrefabFor(string prefabResourcePath)
    {
      try
      {
        var rtn = Resources.Load(prefabResourcePath, typeof(GameObject)) as GameObject;
        if (rtn != null)
        {
          return rtn;
        }

        throw new Exception($"Invalid resource path: {prefabResourcePath}");
      }
      catch (Exception e)
      {
        throw new Exception($"Invalid resource path: {prefabResourcePath}: {e}");
      }
    }
  }
}