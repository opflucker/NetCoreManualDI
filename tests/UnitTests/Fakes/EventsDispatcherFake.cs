using NetCoreManualDI.ApplicationDomain.Events;
using NetCoreManualDI.BusinessDomain.Commons;
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
