using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.ApplicationDomain
{
    internal sealed class CoursesService : ICoursesService
    {
        private readonly ISchoolContext schoolContext;

        public CoursesService(ISchoolContext schoolContext)
        {
            this.schoolContext = schoolContext;
        }

        public Task<Course?> GetByNameAsync(string name)
        {
            return schoolContext.Courses.GetByNameAsync(name);
        }
    }
}
