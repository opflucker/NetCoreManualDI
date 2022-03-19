using NetCoreManualDI.ApplicationDomain.Courses;
using NetCoreManualDI.ApplicationDomain.Students;
using NetCoreManualDI.BusinessDomain.Core.Courses;
using NetCoreManualDI.BusinessDomain.Core.Students;

namespace NetCoreManualDI.ApplicationDomain.School
{
    internal sealed class SchoolService : ISchoolService
    {
        private readonly ISchoolContextWithEvents schoolContext;
        private readonly ICoursesService coursesService;
        private readonly IStudentsService studentsService;

        public SchoolService(
            Func<ISchoolContextWithEvents> schoolContextFactory,
            Func<ICoursesRepository, ICoursesService> coursesServiceFactory,
            Func<IStudentsRepository, IStudentsService> studentsServiceFactory)
        {
            schoolContext = schoolContextFactory();
            coursesService = coursesServiceFactory(schoolContext.School.Courses);
            studentsService = studentsServiceFactory(schoolContext.School.Students);
        }

        public void Dispose()
        {
            schoolContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Initialize()
        {
            var courseNames = new[] { "Math", "Physics", "History" };
            var courses = courseNames.Select(name => new Course(name.ToCourseName())).ToArray();
            foreach (var course in courses)
                schoolContext.School.Courses.Register(course);

            var student = new Student("Otto".ToStudentName(), courses.First());
            schoolContext.School.Students.Register(student);

            await schoolContext.SaveChangesAndDispatchEventsAsync();
        }

        public async Task EnrollStudent(StudentName studentName, CourseName courseName)
        {
            var course = await coursesService.GetByNameAsync(courseName);
            var student = await studentsService.GetByNameAsync(studentName);
            if (student != null && course != null)
            {
                student.EnrollIn(course);
            }

            await schoolContext.SaveChangesAndDispatchEventsAsync();
        }
    }
}
