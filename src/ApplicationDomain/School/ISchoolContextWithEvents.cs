namespace NetCoreManualDI.ApplicationDomain.School
{
    public interface ISchoolContextWithEvents : IDisposable
    {
        ISchoolContext School { get; }

        Task SaveChangesAndDispatchEventsAsync();
    }
}
