using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.ApplicationDomain
{
    internal sealed class SchoolService : ISchoolService
    {
        private readonly IApplicationContext applicationUnitOfWork;
        private readonly ICoursesService coursesService;
        private readonly IStudentsService studentsService;

        public SchoolService(
            Func<IApplicationContext> applicationUnitOfWorkFactory,
            Func<ISchoolContext, ICoursesService> coursesServiceFactory,
            Func<ISchoolContext, IStudentsService> studentsServiceFactory)
        {
            applicationUnitOfWork = applicationUnitOfWorkFactory();
            coursesService = coursesServiceFactory(applicationUnitOfWork.School);
            studentsService = studentsServiceFactory(applicationUnitOfWork.School);
        }

        public void Dispose()
        {
            applicationUnitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Initialize()
        {
            var courseNames = new[] { "Math", "Physics", "History" };
            var courses = courseNames.Select(name => new Course(name.ToCourseName())).ToArray();
            foreach (var course in courses)
                applicationUnitOfWork.School.Courses.Register(course);

            var student = new Student("Otto".ToStudentName(), courses.First());
            applicationUnitOfWork.School.Students.Register(student);

            await applicationUnitOfWork.SaveChangesAsync();
        }

        public async Task EnrollStudent(StudentName studentName, CourseName courseName)
        {
            var course = await coursesService.GetByNameAsync(courseName);
            var student = await studentsService.GetByNameAsync(studentName);
            if (student != null && course != null)
            {
                student.EnrollIn(course);
            }

            await applicationUnitOfWork.SaveChangesAsync();
        }
    }
}
