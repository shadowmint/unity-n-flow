using System.Collections.Generic;
using N.Package.Flow;

namespace Demo
{
  public class DemoInventorySlotComponent : FlowComponent<DemoInventorySlotState, DemoInventorySlotProps>
  {
    public DemoInventorySlotComponent(DemoInventorySlotState state) : base(state)
    {
    }

    public override string ComponentResourcePath()
    {
      return "DemoInventorySlot";
    }

    public override void OnComponentRender()
    {
      // TODO
    }

    public override IEnumerable<IFlowComponent> OnComponentLayout()
    {
      yield break;
    }
  }
}