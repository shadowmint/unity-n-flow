using System;
using UnityEngine;

namespace N.Package.Flow.Samples.Controllers
{
  [System.Serializable]
  public class SimpleControllerState : FlowComponentState
  {
    public GameObject Tickers;
    public DateTime TickerTime;
  }
}