using System.Collections.Generic;
using System.Linq;

namespace N.Package.Flow.Tests.Components
{
  public class FlowTestContainerComponent : FlowComponent<FlowTestContainerComponentState, FlowTestContainerComponentProps>
  {
    public FlowTestContainerComponent(FlowTestContainerComponentState state) : base(state)
    {
    }

    public override string ComponentResourcePath()
    {
      return $"{GetType().Namespace}/{GetType().Name}";
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
}