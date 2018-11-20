using System.Collections.Generic;
using N.Package.Flow;

public class FlowTestPlaceholderComponent : FlowComponent<FlowTestPlaceholderComponentState, FlowTestPlaceholderComponentProps>
{
  public FlowTestPlaceholderComponent(FlowTestPlaceholderComponentState state) : base(state)
  {
  }

  public override void OnComponentRender()
  {
  }
  
  public override IEnumerable<IFlowComponent> OnComponentLayout()
  {
    yield break;
  }
}