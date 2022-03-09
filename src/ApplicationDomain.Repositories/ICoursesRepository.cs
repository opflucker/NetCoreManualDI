using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.ApplicationDomain.Repositories
{
    public interface ICoursesRepository
    {
        ValueTask<Course?> GetByIdAsync(Guid id);
        Task<Course?> GetByNameAsync(String name);
        void Register(Course course);
    }
}
