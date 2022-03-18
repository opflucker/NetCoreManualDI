using NetCoreManualDI.ApplicationDomain.Repositories;

namespace NetCoreManualDI.ApplicationDomain
{
    public interface IApplicationContext : IDisposable
    {
        ISchoolContext School { get; }

        Task SaveChangesAsync();
    }
}
