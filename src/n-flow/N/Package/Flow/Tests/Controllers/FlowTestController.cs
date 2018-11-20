using N.Package.Flow;

public class FlowTestController : FlowController<FlowTestRootComponentState>
{
  protected override IFlowComponent OnComponentLayout()
  {
    return new FlowTestRootComponent(State);
  }
}