using System;
using N.Package.Flow.Dispatchers;
using N.Package.Flow.Samples.Components;

namespace N.Package.Flow.Samples.Controllers
{
  public class SimpleController : FlowController<SimpleControllerState>
  {
    protected override IFlowComponent OnComponentLayout()
    {
      return new TickerComponent(new TickerComponentState()
      {
        Container = () => State.Tickers,
        TimeStamp = State.TickerTime,
        OnTick = (time) =>
        {
          State.TickerTime = time;
          Actions.Update = true;
        }
      });
    }
  }
}