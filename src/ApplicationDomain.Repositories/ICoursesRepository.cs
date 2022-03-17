using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.ApplicationDomain.Repositories
{
    public interface ICoursesRepository
    {
        ValueTask<Course?> GetByIdAsync(Guid id);
        Task<Course?> GetByNameAsync(CourseName name);
        void Register(Course course);
        //void StartTracking(Course course);
    }
}
