using System.Collections.Generic;

namespace N.Package.Flow.Tests.Components
{
  public class FlowTestPlaceholderComponent : FlowComponent<FlowTestPlaceholderComponentState, FlowTestPlaceholderComponentProps>
  {
    public FlowTestPlaceholderComponent(FlowTestPlaceholderComponentState state) : base(state)
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
      yield break;
    }
  }
}