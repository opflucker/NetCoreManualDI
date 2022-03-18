using NetCoreManualDI.BusinessDomain.Commons;

namespace NetCoreManualDI.ApplicationDomain.Events
{
    public interface IEventsDispatcher
    {
        Task DispatchAsync(IEvent @event);
    }
}
