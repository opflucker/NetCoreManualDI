using NetCoreManualDI.Application.Events;
using NetCoreManualDI.Application.School;
using NetCoreManualDI.Application.School.Context;

namespace NetCoreManualDI.Application
{
    public static class Factories
    {
        private static Func<ISchoolContextWithEvents> CreateSchoolContextFactory(Func<ISchoolContext> contextFactory, Func<IEventsDispatcher> eventsDispatcherFactory)
            => () => new SchoolContextWithEvents(contextFactory, eventsDispatcherFactory);

        public static ISchoolService CreateSchoolService(Func<ISchoolContext> contextFactory, Func<IEventsDispatcher> eventsDispatcherFactory)
            => new SchoolService(CreateSchoolContextFactory(contextFactory, eventsDispatcherFactory));
    }
}
