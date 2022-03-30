namespace NetCoreManualDI.Application.School.Context
{
    public interface ISchoolContextWithEvents : IDisposable
    {
        ISchoolContext School { get; }

        Task SaveChangesAndDispatchEventsAsync();
    }
}
