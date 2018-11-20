using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace N.Package.Flow.Utils.Draggables
{
  public class FlowDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
  {
    public bool AllowDrag = true;

    [Tooltip("Drag this root component when this draggable handle is moved")]
    public RectTransform RootComponent;

    public IFlowDragHandler RelatedComponent { get; set; }

    private Raycaster _raycaster;

    private Raycaster Raycaster => _raycaster ?? (_raycaster = new Raycaster(FindCavnas()));

    private Canvas FindCavnas()
    {
      return Traversal.FindComponentInParents<Canvas>(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (RootComponent != null)
      {
        RootComponent.position = (Vector2) RootComponent.position + eventData.delta;
        RelatedComponent?.OnFlowDrag(RootComponent);
      }

      RelatedComponent?.OnDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      RelatedComponent?.OnBeginDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      Debug.Log($"Drag stop! {RelatedComponent}");
      RelatedComponent?.OnEndDrag(eventData);
    }

    public interface IFlowDragHandler : IBeginDragHandler, IEndDragHandler, IDragHandler
    {
      void OnFlowDrag(RectTransform position);
    }

    public IEnumerable<FlowDropZone> FindFlowDropZones(PointerEventData eventData)
    {
      Debug.Log($"Drop zones: {Raycaster.FindComponentsUnderPoint<FlowDropZone>(eventData.position).ToList().Count}");
      return Raycaster.FindComponentsUnderPoint<FlowDropZone>(eventData.position).Where(i => i.AllowDrop(this));
    }
  }
}