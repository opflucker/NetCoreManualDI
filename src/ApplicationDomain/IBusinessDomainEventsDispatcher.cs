using NetCoreManualDI.BusinessDomain.Commons;

namespace NetCoreManualDI.ApplicationDomain
{
    public interface IBusinessDomainEventsDispatcher
    {
        Task DispatchAsync(IEvent @event);
    }
}
