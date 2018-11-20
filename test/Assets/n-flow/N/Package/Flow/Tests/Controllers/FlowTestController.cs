using N.Package.Flow.Tests.Components;

namespace N.Package.Flow.Tests.Controllers
{
  public class FlowTestController : FlowController<FlowTestRootComponentState>
  {
    protected override IFlowComponent OnComponentLayout()
    {
      return new FlowTestRootComponent(State);
    }
  }
}