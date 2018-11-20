using System.Collections.Generic;
using System.Linq;
using N.Package.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

namespace N.Package.Flow.Utils
{
  public class Raycaster
  {
    private readonly EventSystem _eventSystem;
    private GraphicRaycaster _raycaster;

    public Raycaster(Canvas root)
    {
      _raycaster = root.GetComponent<GraphicRaycaster>();
      _eventSystem = EventSystem.current;
    }
    
    public IEnumerable<GameObject> FindObjectsUnderPoint(Vector3 screenPosition)
    {
      var pointerData = new PointerEventData(_eventSystem)
      {
        
        position = Input.mousePosition
      };

      var results = new List<RaycastResult>();
      _raycaster.blockingObjects = GraphicRaycaster.BlockingObjects.All;
      _raycaster.ignoreReversedGraphics = false;
      _raycaster.Raycast(pointerData, results);
      Debug.Log($"Under cursor were {results.Count} targets");
      foreach (var item in results)
      {
        Debug.Log(item);
      }

      return results.Select(i => i.gameObject);
    }

    public IEnumerable<T> FindComponentsUnderPoint<T>(Vector3 screenPosition) where T : Component
    {
      return FindObjectsUnderPoint(screenPosition).Select(target => target.GetComponent<T>()).Where(instance => instance != null);
    }
  }
}