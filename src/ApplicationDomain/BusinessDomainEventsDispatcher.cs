using NetCoreManualDI.BusinessDomain.Commons;

namespace NetCoreManualDI.ApplicationDomain
{
    internal class BusinessDomainEventsDispatcher : IBusinessDomainEventsDispatcher
    {
        public Task DispatchAsync(IEvent @event)
        {
            Console.WriteLine($"Dispatched event: {@event}");
            return Task.CompletedTask;
        }
    }
}
