using NetCoreManualDI.Domain.Core.Courses;

namespace NetCoreManualDI.Application.Courses
{
    public interface ICoursesRepository
    {
        Task<IReadOnlyList<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(Guid id);
        Task<Course?> GetByNameAsync(CourseName name);
        void Register(Course course);
    }
}
