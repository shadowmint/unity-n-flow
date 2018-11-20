namespace N.Package.Flow.Infrastructure
{
  public class FlowVirtualComponentHeirarchy
  {
    private FlowVirtualComponent _root;

    public FlowVirtualComponentHeirarchy(FlowComponentBase componentBase, IFlowDispatcher dispatcher)
    {
      _root = new FlowVirtualComponent(componentBase, dispatcher);
    }

    public void Update(IFlowComponent component)
    {
      _root?.Update(component);
    }

    public FlowComponentDebugHeirarchy GenerateDebugHeirarchy()
    {
      return _root?.GenerateDebugHeirarchy();
    }

    public void Dispose()
    {
      _root.Unmount();
      _root = null;
    }
  }
}