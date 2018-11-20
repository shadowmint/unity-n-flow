#if N_FLOW_TESTS
using System.Collections.Generic;
using N.Package.Core;
using N.Package.Core.Tests;
using NUnit.Framework;

namespace N.Package.Flow.Editor
{
  public class FlowTests : TestCase
  {
    [Test]
    public void TestControllerCanBuildLayout()
    {
      var controller = this.SpawnComponent<FlowTestController>();
      var state = new FlowTestRootComponentState()
      {
        Items = new List<FlowTestPlaceholderComponentState>()
        {
          new FlowTestPlaceholderComponentState(),
          new FlowTestPlaceholderComponentState(),
          new FlowTestPlaceholderComponentState(),
        },        
        Container = () => controller.gameObject
      };
      var dispatcher = new MockFlowDispatcher();

      controller.SetDispatcher(dispatcher);
      controller.State = state;
      controller.Actions.Mount = true;
      controller.Actions.Update = true;
      controller.Actions.Debug = true;
      controller.Update();

      var heirarhcy = controller.DebugData;
      Assert(heirarhcy.Identity == "FlowTestRootComponentState");
      Assert(heirarhcy.Children[0].Identity == "FlowTestContainerComponentState");
      Assert(heirarhcy.Children[0].Children[0].Identity == "FlowTestPlaceholderComponentState");
      Assert(heirarhcy.Children[0].Children.Count == 3);

      this.TearDown();
    }

    [Test]
    public void TestControllerCanControlUpdate()
    {
      var controller = this.SpawnComponent<FlowTestController>();
      var state = new FlowTestRootComponentState()
      {
        SpawnMaybeUpdate = true,
        MaybeUpdateShouldUpdate = true,
        Container = () => controller.gameObject
      };
      var dispatcher = new MockFlowDispatcher();

      controller.SetDispatcher(dispatcher);
      controller.State = state;
      controller.Actions.Mount = true;
      controller.Actions.Update = true;
      controller.Actions.Debug = true;
      controller.Update();

      var heirarhcy = controller.DebugData;
      Assert(heirarhcy.Identity == "FlowTestRootComponentState");
      Assert(heirarhcy.Children[0].Identity == "FlowTestMaybeUpdateComponentState");
      Assert(heirarhcy.Children[0].Updated);

      controller.State.MaybeUpdateShouldUpdate = false;
      controller.Actions.Update = true;
      controller.Actions.Debug = true;
      controller.Update();
      
      heirarhcy = controller.DebugData;
      Assert(heirarhcy.Identity == "FlowTestRootComponentState");
      Assert(heirarhcy.Children[0].Identity == "FlowTestMaybeUpdateComponentState");
      Assert(!heirarhcy.Children[0].Updated);
      
      this.TearDown();
    }
  }
}
#endif