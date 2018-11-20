using System.Collections.Generic;
using System.Linq;

namespace N.Package.Flow.Tests.Components
{
  public class FlowTestRootComponent : FlowComponent<FlowTestRootComponentState, FlowTestRootComponentProps>
  {
    public FlowTestRootComponent(FlowTestRootComponentState state) : base(state)
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
      if (State.Items?.Count > 0)
      {
        yield return new FlowTestContainerComponent(new FlowTestContainerComponentState()
        {
          Items = State?.Items?.Select(i => new FlowTestPlaceholderComponent(i)).ToList()
        });  
      }
      if (State.SpawnMaybeUpdate)
      {
        yield return new FlowTestMaybeUpdateComponent(new FlowTestMaybeUpdateComponentState()
        {
          Update = State.MaybeUpdateShouldUpdate
        });
      }
    }
  }
}