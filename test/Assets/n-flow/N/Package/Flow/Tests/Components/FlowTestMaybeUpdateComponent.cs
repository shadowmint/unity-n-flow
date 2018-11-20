using System.Collections.Generic;

namespace N.Package.Flow.Tests.Components
{
  public class FlowTestMaybeUpdateComponent : FlowComponent<FlowTestMaybeUpdateComponentState, FlowTestMaybeUpdateComponentProps>
  {
    public FlowTestMaybeUpdateComponent(FlowTestMaybeUpdateComponentState state) : base(state)
    {
    }

    public override IEnumerable<IFlowComponent> OnComponentLayout()
    {
      yield break;
    }

    public override string ComponentResourcePath()
    {
      return $"{GetType().Namespace}/{GetType().Name}";
    }

    public override void OnComponentRender()
    {
    }
    
    public override bool ComponentShouldUpdate()
    {
      var update = State.Update;
      State.Update = false;
      return update;
    }
  }
}