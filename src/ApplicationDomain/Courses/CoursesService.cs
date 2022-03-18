using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Core.Courses;

namespace NetCoreManualDI.ApplicationDomain.Courses
{
    internal sealed class CoursesService : ICoursesService
    {
        private readonly ICoursesRepository courses;

        public CoursesService(ICoursesRepository courses)
        {
            this.courses = courses;
        }

        public Task<Course?> GetByNameAsync(CourseName name)
        {
            return courses.GetByNameAsync(name);
        }
    }
}
