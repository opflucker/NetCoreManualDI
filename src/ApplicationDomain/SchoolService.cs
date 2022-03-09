using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.ApplicationDomain
{
    internal sealed class SchoolService : ISchoolService
    {
        private readonly ISchoolContext schoolContext;
        private readonly ICoursesService coursesService;
        private readonly IStudentsService studentsService;

        public SchoolService(
            Func<ISchoolContext> schoolContextFactory,
            Func<ISchoolContext, ICoursesService> coursesServiceFactory,
            Func<ISchoolContext, IStudentsService> studentsServiceFactory)
        {
            schoolContext = schoolContextFactory();
            coursesService = coursesServiceFactory(schoolContext);
            studentsService = studentsServiceFactory(schoolContext);
        }

        public void Dispose()
        {
            schoolContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Initialize()
        {
            var course = new Course("Math");
            schoolContext.Courses.Register(course);

            var student = new Student("Otto", course);
            schoolContext.Students.Register(student);

            await schoolContext.SaveChangesAsync();
        }

        public async Task EnrollStudent(string studentName, string courseName)
        {
            var course = await coursesService.GetByNameAsync(courseName);
            var student = await studentsService.GetByNameAsync(studentName);
            if (student != null && course != null)
            {
                student.EnrollIn(course);
            }

            await schoolContext.SaveChangesAsync();
        }
    }
}
