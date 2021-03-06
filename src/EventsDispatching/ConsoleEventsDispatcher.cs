using NetCoreManualDI.Application.Events;
using NetCoreManualDI.Domain.Commons;

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
