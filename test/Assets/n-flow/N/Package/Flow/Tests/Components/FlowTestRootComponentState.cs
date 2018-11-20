using System.Collections.Generic;

namespace N.Package.Flow.Tests.Components
{
  public class FlowTestRootComponentState : FlowComponentState
  {
    public List<FlowTestPlaceholderComponentState> Items { get; set; }
    public bool SpawnMaybeUpdate { get; set; }
    public bool MaybeUpdateShouldUpdate { get; set; }
  }
}