using System;
using System.Collections.Generic;
using System.Linq;
using N.Package.Flow.Dispatchers;
using UnityEngine;

namespace N.Package.Flow.Infrastructure
{
  public class FlowPrefabFactory : IFlowPrefabFactory
  {
    private readonly FlowComponentBase _componentBase;

    private readonly IDictionary<Type, GameObject> _resolved = new Dictionary<Type, GameObject>();
    
    public FlowPrefabFactory(FlowComponentBase componentBase)
    {
      _componentBase = componentBase;
    }

    public GameObject Load(IFlowComponent component)
    {
      var props = component.PropertiesType;
      if (props == null) return null;
      
      if (_resolved.ContainsKey(props))
      {
        return _resolved[props];
      }

      var match = FindComponentWithProperties(props);
      if (match == null)
      {
        throw new Exception($"No assigned component on controller {_componentBase} has a binding for property type {props}");
      }

      _resolved[props] = match;
      return match;
    }

    private GameObject FindComponentWithProperties(Type props)
    {
      return (from target in _componentBase.Components where target.GetType() == props select target.gameObject).FirstOrDefault();
    }
  }
}