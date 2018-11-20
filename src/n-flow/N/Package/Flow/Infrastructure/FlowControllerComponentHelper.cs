using System;
using System.Collections.Generic;
using System.Linq;
using N.Package.Flow.Infrastructure;
using UnityEngine;

namespace N.Package.Flow
{
  [ExecuteInEditMode]
  public class FlowControllerComponentHelper : MonoBehaviour
  {
    [Tooltip("Drag a component in here in the editor to add it to the controller")]
    public FlowComponentProperties Component;

    private FlowComponentBase _controller;

    [Tooltip("Cleanup")]
    public bool Clean;
    
    void Update()
    {
#if UNITY_EDITOR
      CleanupComponentList();
      AddComponentToList();
#endif
    }

    private void CleanupComponentList()
    {
      if (Clean)
      {
        var controller = GetComponent<FlowComponentBase>();
        if (controller == null)
        {
          throw new Exception("You must add a controller to use this helper!");
        }
        
        var current = controller.Components?.ToList() ?? new List<FlowComponentProperties>();
        current = current.Where(i => i != null).OrderBy(i => i.GetType().Name).ToList();
        
        controller.Components = current.ToArray();
        Clean = false;
      }
    }

    private void AddComponentToList()
    {
      if (Component == null) return;

      var controller = GetComponent<FlowComponentBase>();
      if (controller == null)
      {
        throw new Exception("You must add a controller to use this helper!");
      }

      var current = controller.Components?.ToList() ?? new List<FlowComponentProperties>();
      current = current.Where(i => i.GetType() != Component.GetType()).ToList();
      current.Add(Component);
      controller.Components = current.ToArray();
  
      Component = null;
      Clean = true;
    }
  }
}