using NetCoreManualDI.Application.Courses;
using NetCoreManualDI.Application.Students;
using NetCoreManualDI.Domain.Commons;

namespace NetCoreManualDI.Application.School.Context
{
    public interface ISchoolContext : IDisposable
    {
        ICoursesRepository Courses { get; }
        IStudentsRepository Students { get; }

        Task<IReadOnlyList<RootAggregate>> SaveChangesAsync();
    }
}