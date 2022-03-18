using NetCoreManualDI.BusinessDomain.Commons;

namespace NetCoreManualDI.ApplicationDomain.Repositories
{
    public interface ISchoolContext : IDisposable
    {
        ICoursesRepository Courses { get; }
        IStudentsRepository Students { get; }

        Task<IReadOnlyList<RootAggregate>> SaveChangesAsync();
    }
}