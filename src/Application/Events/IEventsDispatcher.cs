using NetCoreManualDI.Domain.Commons;

namespace NetCoreManualDI.Application.Events
{
    public interface IEventsDispatcher
    {
        Task DispatchAsync(IEvent @event);
    }
}
