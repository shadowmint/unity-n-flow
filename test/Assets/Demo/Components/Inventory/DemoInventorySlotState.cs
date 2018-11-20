using N.Package.Flow;

namespace Demo
{
  public class DemoInventorySlotState : FlowComponentState
  {
    public int SlotId { get; set; }
    public override string Identity => $"DemoInventoryBarState.{SlotId}";
  }
}