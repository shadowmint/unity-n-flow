using System.Collections.Generic;
using N.Package.Flow;

namespace Demo
{
  public class DemoInventoryBarComponent : FlowComponent<DemoInventoryBarState, DemoInventoryBarProps>
  {
    public DemoInventoryBarComponent(DemoInventoryBarState state) : base(state)
    {
    }

    public override string ComponentResourcePath()
    {
      return "DemoInventoryBar";
    }

    public override void OnComponentRender()
    {
    }

    public override IEnumerable<IFlowComponent> OnComponentLayout()
    {
      foreach (var slot in State.Slots)
      {
        slot.Container = () => Properties.SlotContainer;
        yield return new DemoInventorySlotComponent(slot);
      }
    }
  }
}