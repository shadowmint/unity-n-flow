using System.Collections.Generic;
using System.Runtime.InteropServices;
using N.Package.Flow;
using N.Package.Flow.Utils;
using N.Package.Flow.Utils.Draggables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo
{
  public class DraggableFrameComponent : FlowComponent<DraggableFrameState, DraggableFrameProps>, FlowDraggable.IFlowDragHandler
  {
    public DraggableFrameComponent(DraggableFrameState state) : base(state)
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
      if (State.Content == null)
      {
        yield break;
      }

      State.Content.State.Container = () => Properties.Container;
      yield return State.Content;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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