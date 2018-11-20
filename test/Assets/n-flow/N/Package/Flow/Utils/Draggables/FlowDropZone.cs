using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace N.Package.Flow.Utils.Draggables
{
  /// <summary>
  /// This is a drop zone that you may be able to drop a draggable on to.
  /// </summary>
  public class FlowDropZone : MonoBehaviour
  {
    public FlowDropZoneBehaviour Mode = FlowDropZoneBehaviour.DropAny;

    public FlowDropZoneTagConfig Tags;

    public Func<FlowDraggable, bool> CustomFilter;

    public bool AllowDrop(FlowDraggable draggable)
    {
      switch (Mode)
      {
        case FlowDropZoneBehaviour.Disabled:
          return false;
        case FlowDropZoneBehaviour.DropAny:
          return true;
        case FlowDropZoneBehaviour.DropFiltered:
          return AcceptDraggable(draggable);
        case FlowDropZoneBehaviour.DropCustom:
          return AcceptDraggable(draggable, CustomFilter);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private bool AcceptDraggable(FlowDraggable draggable, Func<FlowDraggable, bool> customFilter)
    {
      return customFilter != null && customFilter(draggable);
    }

    private bool AcceptDraggable(FlowDraggable draggable)
    {
      var accept = Tags.AcceptedTags?.Any(draggable.CompareTag);
      var reject = Tags.RejectedTags?.Any(draggable.CompareTag);
      return accept.HasValue && accept.Value && (reject == null || (!reject.Value));
    }

    public struct FlowDropZoneTagConfig
    {
      [Tooltip("When in DropFiltered, accept objects with this tag")]
      public string[] AcceptedTags;

      [Tooltip("When in DropFiltered, reject objects with this tag")]
      public string[] RejectedTags;
    }
  }
}