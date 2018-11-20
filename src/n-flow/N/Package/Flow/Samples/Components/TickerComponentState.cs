using System;

namespace N.Package.Flow.Samples.Components
{
  public class TickerComponentState : FlowComponentState
  {
    public DateTime TimeStamp { get; set; }
    public Action<DateTime> OnTick { get; set; }
  }
}