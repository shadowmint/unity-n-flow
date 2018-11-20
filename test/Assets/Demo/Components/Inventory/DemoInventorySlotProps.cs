using N.Package.Flow;
using UnityEngine.UI;

namespace Demo
{
  public class DemoInventorySlotProps : FlowComponentProperties
  {
    public Image Icon;
    public Text Count;
    public DemoDraggableInventoryIconState Target { get; set; }
  }
}