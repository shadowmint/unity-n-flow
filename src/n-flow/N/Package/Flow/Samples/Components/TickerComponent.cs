using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace N.Package.Flow.Samples.Components
{
  public class TickerComponent : FlowComponent<TickerComponentState, TickerComponentProps>
  {
    public TickerComponent(TickerComponentState state) : base(state)
    {
    }

    public override void OnComponentRender()
    {
      Properties.TickerText.text = State.TimeStamp.ToString(CultureInfo.InvariantCulture);
    }

    public override IEnumerable<IFlowComponent> OnComponentLayout()
    {
      yield break;
    }

    public override void OnComponentDidMount(FlowComponentProperties props, bool justMounted)
    {
      base.OnComponentDidMount(props, justMounted);
      if (justMounted)
      {
        Properties.OnTick = () =>
        {
          State.OnTick(DateTime.Now);      
        };
      }
    }

    public override void OnComponentWillUnmount()
    {
      base.OnComponentWillUnmount();
      if (Properties != null)
      {
        Properties.OnTick = null;
      }
    }
  }
}