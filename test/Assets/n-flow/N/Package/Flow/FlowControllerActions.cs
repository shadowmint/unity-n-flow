namespace N.Package.Flow
{
  [System.Serializable]
  public class FlowControllerActions
  {
    /// <summary>
    /// If true, this controller will mount its root component.
    /// </summary>
    public bool Mount = true;

    /// <summary>
    /// If true, this controller will call SetState with its own state.
    /// </summary>
    public bool Update = true;

    /// <summary>
    /// If true, this controller will rebuild the debug state.
    /// </summary>
    public bool Debug;
  }
}