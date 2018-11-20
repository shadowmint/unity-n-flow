using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using N.Package.Flow;
using N.Package.Flow.Utils;
using N.Package.Flow.Utils.Draggables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo
{
  public class DemoDraggableInventoryIcon : FlowComponent<DemoDraggableInventoryIconState, DemoDraggableInventoryIconProps>, FlowDraggable.IFlowDragHandler
  {
    public DemoDraggableInventoryIcon(DemoDraggableInventoryIconState state) : base(state)
    {
    }

    public override string ComponentResourcePath()
    {
      return GetType().Name;
    }

    public override void OnComponentRender()
    {
    }

    public override void OnComponentDidMount(FlowComponentProperties props)
    {
      base.OnComponentDidMount(props);
      var draggable = Properties.gameObject.GetComponentInChildren<FlowDraggable>();
      draggable.RelatedComponent = this;      
    }

    public override IEnumerable<IFlowComponent> OnComponentLayout()
    {
      yield break;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      var draggable = Properties.gameObject.GetComponentInChildren<FlowDraggable>();
      var target = draggable.FindFlowDropZones(eventData).FirstOrDefault();
      Debug.Log($"Found {target} targets");
      if (target != null)
      {
        var iconSpace = target.GetComponent<DemoInventorySlotProps>();
        if (iconSpace != null)
        {
          iconSpace.Target = State;
          Debug.Log("Dropped on target!");
        }
      }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnFlowDrag(RectTransform position)
    {
      State.Position = position.transform.position;
    }
  }
}