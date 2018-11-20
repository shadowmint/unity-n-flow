using System.Collections.Generic;
using System.Linq;
using N.Package.Flow;
using UnityEngine;

public class FlowTestContainerComponent : FlowComponent<FlowTestContainerComponentState, FlowTestContainerComponentProps>
{
  public FlowTestContainerComponent(FlowTestContainerComponentState state) : base(state)
  {
  }

  public override void OnComponentRender()
  {
  }

  public override IEnumerable<IFlowComponent> OnComponentLayout()
  {
    return State.Items.Select(i =>
    {
      i.State.Container = () => Properties.Container;
      return i;
    });
  }
}