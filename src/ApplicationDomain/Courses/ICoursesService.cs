using NetCoreManualDI.BusinessDomain.Core.Courses;

namespace NetCoreManualDI.ApplicationDomain.Courses
{
    public interface ICoursesService
    {
        Task<Course?> GetByNameAsync(CourseName name);
    }
}