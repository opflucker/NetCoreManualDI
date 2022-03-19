using NetCoreManualDI.BusinessDomain.Core.Courses;

namespace NetCoreManualDI.ApplicationDomain.Courses
{
    public interface ICoursesRepository
    {
        ValueTask<Course?> GetByIdAsync(Guid id);
        Task<Course?> GetByNameAsync(CourseName name);
        void Register(Course course);
    }
}
