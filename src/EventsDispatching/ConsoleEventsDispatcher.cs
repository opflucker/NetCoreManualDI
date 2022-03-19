using NetCoreManualDI.ApplicationDomain.Events;
using NetCoreManualDI.BusinessDomain.Commons;

namespace NetCoreManualDI.EventsDispatching
{
    internal class ConsoleEventsDispatcher : IEventsDispatcher
    {
        public Task DispatchAsync(IEvent @event)
        {
            Console.WriteLine($"Dispatched event: {@event}");
            return Task.CompletedTask;
        }
    }
}
