using NetCoreManualDI.ApplicationDomain.Courses;
using NetCoreManualDI.ApplicationDomain.Students;
using NetCoreManualDI.BusinessDomain.Commons;

namespace NetCoreManualDI.ApplicationDomain.School
{
    public interface ISchoolContext : IDisposable
    {
        ICoursesRepository Courses { get; }
        IStudentsRepository Students { get; }

        Task<IReadOnlyList<RootAggregate>> SaveChangesAsync();
    }
}