using NetCoreManualDI.Application.Events;

namespace NetCoreManualDI.Application.School.Context
{
    public class SchoolContextWithEvents : ISchoolContextWithEvents
    {
        private readonly ISchoolContext schoolContext;
        private readonly IEventsDispatcher eventsDispatcher;

        public SchoolContextWithEvents(Func<ISchoolContext> schoolContextFactory, Func<IEventsDispatcher> eventsDispatcherFactory)
        {
            schoolContext = schoolContextFactory();
            eventsDispatcher = eventsDispatcherFactory();
        }

        public ISchoolContext School => schoolContext;

        public void Dispose()
        {
            schoolContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAndDispatchEventsAsync()
        {
            var savedAggregates = await schoolContext.SaveChangesAsync();

            foreach (var aggregate in savedAggregates.Where(a => a.Events.Any()))
            {
                foreach (var @event in aggregate.Events)
                {
                    await eventsDispatcher.DispatchAsync(@event);
                }

                aggregate.ClearEvents();
            }
        }
    }
}
