using NetCoreManualDI.Application.Events;
using NetCoreManualDI.Domain.Commons;
using System.Threading.Tasks;

namespace NetCoreManualDI.UnitTests.Fakes
{
    internal class EventsDispatcherFake : IEventsDispatcher
    {
        public Task DispatchAsync(IEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
