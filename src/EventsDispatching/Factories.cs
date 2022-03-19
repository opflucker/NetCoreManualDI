using NetCoreManualDI.ApplicationDomain.Events;

namespace NetCoreManualDI.EventsDispatching
{
    public static class Factories
    {
        public static IEventsDispatcher ForEventsDispatcher()
            => new ConsoleEventsDispatcher();
    }
}
