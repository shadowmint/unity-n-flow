using System;
using N.Package.Flow.Infrastructure;
using UnityEngine;

namespace N.Package.Flow.Dispatchers
{
  public class FlowDispatcher : IFlowDispatcher
  {
    private const int MaxAttemptsToResolveComponent = 50;

    private readonly FlowPrefabFactory _factory;

    public FlowDispatcher(FlowPrefabFactory factory)
    {
      _factory = factory;
    }

    public void DestroyComponentInstance(FlowVirtualComponent virtualComponent)
    {
      if (virtualComponent.Instance == null) return;
      virtualComponent.Instance.SetActive(false);
      UnityEngine.Object.Destroy(virtualComponent.Instance);
      virtualComponent.Instance = null;
    }

    public void CreateComponentInstance(FlowVirtualComponent virtualComponent, IFlowComponentBaseProvider provider)
    {
      if (virtualComponent.Instance != null) return;

      // If this is a state only component, it has no prefab
      var prefabTemplate = GetPrefabTemplate(virtualComponent, provider);

      // If the parent is NULL, don't spawn a component instance at all.
      var container = virtualComponent.Component?.State.Container?.Invoke();
      if (container == null)
      {
        if (prefabTemplate != null)
        {
          throw new Exception(
            $"Component {virtualComponent.Component} with template {prefabTemplate} had no Container to spawn into!");
        }

        virtualComponent.Instance = null;
        return;
      }

      virtualComponent.Instance = SpawnInstance(prefabTemplate, container);
    }

    private GameObject GetPrefabTemplate(FlowVirtualComponent virtualComponent, IFlowComponentBaseProvider parent)
    {
      if (virtualComponent.PropertiesType == null) return null;
      try
      {
        return _factory.Load(virtualComponent.Component);
      }
      catch (Exception)
      {
        // Walk back up the chain of components
        var attempts = 0;
        var root = parent;
        while (root != null)
        {
          try
          {
            var providerBase = root.ComponentBase(this);
            var factory = providerBase.ComponentFactory;
            return factory.Load(virtualComponent.Component);
          }
          catch (Exception)
          {
            attempts += 1;
            if (attempts > MaxAttemptsToResolveComponent)
            {
              throw;
            }

            root = root.Parent;
          }
        }
      }
      
      throw new Exception($"Unable to resolve component instance: {virtualComponent.Component}");
    }

    public FlowComponentProperties GetProperties(FlowVirtualComponent virtualComponent)
    {
      var propType = virtualComponent.Component?.PropertiesType;
      if (virtualComponent.Instance != null)
      {
        var rtn = virtualComponent.Instance.GetComponent(propType) as FlowComponentProperties;
        if (rtn == null)
        {
          Debug.LogWarning($"Missing {propType} on {virtualComponent.Instance}");
        }

        return virtualComponent.Instance.GetComponent(propType) as FlowComponentProperties;
      }

      return null;
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
  }
}