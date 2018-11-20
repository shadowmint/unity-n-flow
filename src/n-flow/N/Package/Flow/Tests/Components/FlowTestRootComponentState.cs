using System.Collections.Generic;
using N.Package.Flow;

public class FlowTestRootComponentState : FlowComponentState
{
  public List<FlowTestPlaceholderComponentState> Items { get; set; }
  public bool SpawnMaybeUpdate { get; set; }
  public bool MaybeUpdateShouldUpdate { get; set; }
}