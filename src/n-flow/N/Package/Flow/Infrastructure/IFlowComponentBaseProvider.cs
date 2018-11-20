namespace N.Package.Flow.Infrastructure
{
  public interface IFlowComponentBaseProvider
  {
    FlowComponentBase ComponentBase(IFlowDispatcher dispatcher);
    IFlowComponentBaseProvider Parent { get; }
  }
}