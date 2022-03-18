using NetCoreManualDI.ApplicationDomain.Courses;
using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.ApplicationDomain.Students;
using NetCoreManualDI.BusinessDomain.Core.Courses;
using NetCoreManualDI.BusinessDomain.Core.Students;

namespace NetCoreManualDI.ApplicationDomain.School
{
    internal sealed class SchoolService : ISchoolService
    {
        private readonly IApplicationContext applicationContext;
        private readonly ICoursesService coursesService;
        private readonly IStudentsService studentsService;

        public SchoolService(
            Func<IApplicationContext> applicationContextFactory,
            Func<ISchoolContext, ICoursesService> coursesServiceFactory,
            Func<ISchoolContext, IStudentsService> studentsServiceFactory)
        {
            applicationContext = applicationContextFactory();
            coursesService = coursesServiceFactory(applicationContext.School);
            studentsService = studentsServiceFactory(applicationContext.School);
        }

        public void Dispose()
        {
            applicationContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Initialize()
        {
            var courseNames = new[] { "Math", "Physics", "History" };
            var courses = courseNames.Select(name => new Course(name.ToCourseName())).ToArray();
            foreach (var course in courses)
                applicationContext.School.Courses.Register(course);

            var student = new Student("Otto".ToStudentName(), courses.First());
            applicationContext.School.Students.Register(student);

            await applicationContext.SaveChangesAsync();
        }

        public async Task EnrollStudent(StudentName studentName, CourseName courseName)
        {
            var course = await coursesService.GetByNameAsync(courseName);
            var student = await studentsService.GetByNameAsync(studentName);
            if (student != null && course != null)
            {
                student.EnrollIn(course);
            }

            await applicationContext.SaveChangesAsync();
        }
    }
}
