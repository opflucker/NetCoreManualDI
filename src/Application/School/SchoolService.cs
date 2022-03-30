using NetCoreManualDI.Application.School.Context;
using NetCoreManualDI.Application.School.Dtos;
using NetCoreManualDI.Domain.Core.Courses;
using NetCoreManualDI.Domain.Core.Students;

namespace NetCoreManualDI.Application.School
{
    internal sealed class SchoolService : ISchoolService
    {
        private readonly ISchoolContextWithEvents context;

        public SchoolService(Func<ISchoolContextWithEvents> createContext)
        {
            context = createContext();
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IReadOnlyList<CourseDto>> GetAllCourses()
        {
            var courses = await context.School.Courses.GetAllAsync();
            return courses.Select(c => c.ToDto()).ToList();
        }

        public async Task<IReadOnlyList<StudentDto>> GetAllStudents()
        {
            var students = await context.School.Students.GetAllAsync();
            return students.Select(s => s.ToDto()).ToList();
        }

        public async Task<CourseDto> RegisterCourse(RegisterCourseDto dto)
        {
            var course = dto.ToCourse();
            context.School.Courses.Register(course);
            await context.SaveChangesAndDispatchEventsAsync();
            return course.ToDto();
        }

        public async Task<StudentDto?> RegisterStudent(RegisterStudentDto dto)
        {
            var student = await dto.ToStudent(context.School.Courses);
            if (student == null)
                return null;

            context.School.Students.Register(student);
            await context.SaveChangesAndDispatchEventsAsync();
            return student.ToDto();
        }

        public async Task EnrollStudent(EnrollStudentDto dto)
        {
            var course = await context.School.Courses.GetByNameAsync(dto.CourseName.ToCourseName());
            var student = await context.School.Students.GetByNameAsync(dto.StudentName.ToStudentName());
            if (student == null || course == null)
                return;

            student.EnrollIn(course);
            await context.SaveChangesAndDispatchEventsAsync();
        }
    }
}
