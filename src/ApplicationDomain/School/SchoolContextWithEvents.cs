using NetCoreManualDI.ApplicationDomain.Events;
using NetCoreManualDI.ApplicationDomain.Repositories;

namespace NetCoreManualDI.ApplicationDomain
{
    public class SchoolContextWithEvents : ISchoolContextWithEvents
    {
        private readonly ISchoolContext schoolContext;
        private readonly IEventsDispatcher eventsDispatcher;

        public SchoolContextWithEvents(Func<ISchoolContext> schoolContext, IEventsDispatcher eventsDispatcher)
        {
            this.schoolContext = schoolContext();
            this.eventsDispatcher = eventsDispatcher;
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
