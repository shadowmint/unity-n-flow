using System.Collections.Generic;

namespace N.Package.Flow
{
  public interface IFlowComponent
  {
    IEnumerable<IFlowComponent> OnComponentLayout();
    FlowComponentState State { get; }
    System.Type PropertiesType { get; }
    bool ComponentShouldUpdate();
    void OnComponentDidMount(FlowComponentProperties properties);
    void OnComponentWillUnmount();
    void OnComponentRender();
    string ComponentResourcePath();
  }
}