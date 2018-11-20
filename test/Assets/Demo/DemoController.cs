using N.Package.Flow;
using UnityEngine;

namespace Demo
{
  public class DemoController : FlowController<DemoControllerState>
  {
    protected override IFlowComponent OnComponentLayout()
    {
      return new DemoComponent(State);
    }
  }
}