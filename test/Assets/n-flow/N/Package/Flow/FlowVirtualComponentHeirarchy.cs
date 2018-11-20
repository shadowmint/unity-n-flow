using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace N.Package.Flow
{
  public class FlowVirtualComponentHeirarchy
  {
    private FlowVirtualComponent _root;

    public FlowVirtualComponentHeirarchy(IFlowDispatcher dispatcher)
    {
      _root = new FlowVirtualComponent(dispatcher);
    }

    public void Update(IFlowComponent component)
    {
      _root?.Update(component, null);
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