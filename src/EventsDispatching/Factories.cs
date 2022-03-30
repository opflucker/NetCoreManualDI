using NetCoreManualDI.Application.Events;

namespace NetCoreManualDI.EventsDispatching
{
    public static class Factories
    {
        public static IEventsDispatcher CreateEventsDispatcher()
            => new ConsoleEventsDispatcher();
    }
}
