using System;
using UnityEngine;
using UnityEngine.UI;

namespace N.Package.Flow.Samples.Components
{
  public class TickerComponentProps : FlowComponentProperties
  {
    public Text TickerText;

    public Action OnTick { get; set; }

    private float _elapsed;

    public void Update()
    {
      _elapsed += Time.deltaTime;
      if (_elapsed > 1f)
      {
        OnTick?.Invoke();
        _elapsed = 0f;
      }
    }
  }
}