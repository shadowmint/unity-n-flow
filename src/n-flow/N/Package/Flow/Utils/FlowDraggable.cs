using UnityEngine;
using UnityEngine.EventSystems;

namespace N.Package.Flow.Utils
{
  public class FlowDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
  {
    public bool AllowDrag = true;

    [Tooltip("Drag this root component when this draggable handle is moved")]
    public RectTransform RootComponent;

    public IFlowDragHandler RelatedComponent { get; set; }

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
      RelatedComponent?.OnEndDrag(eventData);
    }

    public interface IFlowDragHandler : IBeginDragHandler, IEndDragHandler, IDragHandler
    {
      void OnFlowDrag(RectTransform position);
    }
  }
}