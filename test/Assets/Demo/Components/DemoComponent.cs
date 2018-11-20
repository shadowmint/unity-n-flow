using System.Collections.Generic;
using N.Package.Flow;
using N.Package.Flow.Samples.Components;

namespace Demo
{
  public class DemoComponent : FlowComponent<DemoControllerState, DemoComponentProps>
  {
    public DemoComponent(DemoControllerState state) : base(state)
    {
    }

    public override string ComponentResourcePath()
    {
      return null;
    }

    public override void OnComponentRender()
    {
    }

    public override IEnumerable<IFlowComponent> OnComponentLayout()
    {
      yield return new DraggableFrameComponent(new DraggableFrameState()
      {
        Container = () => State.Root,
        Content = new TickerComponent(new TickerComponentState()
        {
          TimeStamp = State.Time,
          OnTick = (time) =>
          {
            State.Time = time;
            State.Controller.Actions.Update = true;
          },
        })
      });
      yield return new DemoInventoryBarComponent(new DemoInventoryBarState()
      {
        Container = () => State.Root,
        Slots = new List<DemoInventorySlotState>()
        {
          new DemoInventorySlotState() {SlotId = 0},
          new DemoInventorySlotState() {SlotId = 1},
          new DemoInventorySlotState() {SlotId = 2},
          new DemoInventorySlotState() {SlotId = 3},
          new DemoInventorySlotState() {SlotId = 4},
        }
      });
      yield return new DemoDraggableInventoryIcon(new DemoDraggableInventoryIconState()
      {
        Container = () => State.Root,
      });
    }
  }
}