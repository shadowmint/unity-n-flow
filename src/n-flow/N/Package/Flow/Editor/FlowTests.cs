#if N_FLOW_TESTS
using System.Collections.Generic;
using N.Package.Flow.Tests.Components;
using N.Package.Test;
using NUnit.Framework;
using UnityEngine;

namespace N.Package.Flow.Editor
{
  public class FlowTests : TestCase
  {
    [Test]
    public void TestControllerCanBuildLayout()
    {
      var controller = SpawnComponent<FlowTestController>();
      var state = new FlowTestRootComponentState()
      {
        Items = new List<FlowTestPlaceholderComponentState>()
        {
          new FlowTestPlaceholderComponentState() { Identity = "1"},
          new FlowTestPlaceholderComponentState() { Identity = "2"},
          new FlowTestPlaceholderComponentState() { Identity = "3"},
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
      Assert(heirarhcy.Children[0].Children[0].Identity == "1");
      Assert(heirarhcy.Children[0].Children.Count == 3);

      TearDown();
    }

    [Test]
    public void TestControllerCanControlUpdate()
    {
      var controller = SpawnComponent<FlowTestController>();
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
      Assert(heirarhcy.Children[0].Identity == typeof(FlowTestMaybeUpdateComponentState).FullName);
      Assert(heirarhcy.Children[0].Updated);

      controller.State.MaybeUpdateShouldUpdate = false;
      controller.Actions.Update = true;
      controller.Actions.Debug = true;
      controller.Update();
      
      heirarhcy = controller.DebugData;
      Assert(heirarhcy.Identity == typeof(FlowTestRootComponentState).FullName);
      Assert(heirarhcy.Children[0].Identity == typeof(FlowTestMaybeUpdateComponentState).FullName);
      Assert(!heirarhcy.Children[0].Updated);
      
      TearDown();
    }
  }
}
#endif