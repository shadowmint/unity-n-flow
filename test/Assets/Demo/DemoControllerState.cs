using System;
using N.Package.Flow;
using UnityEngine;

namespace Demo
{
  [System.Serializable]
  public class DemoControllerState : FlowComponentState
  {
    public DemoController Controller;
    public DateTime Time = DateTime.Now;
    public GameObject Root;
  }
}