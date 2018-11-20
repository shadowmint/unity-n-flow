using System.Collections.Generic;
using N.Package.Flow;

namespace Demo
{
  public class DemoInventoryBarState : FlowComponentState
  {
    public IEnumerable<DemoInventorySlotState> Slots { get; set; }
  }
}