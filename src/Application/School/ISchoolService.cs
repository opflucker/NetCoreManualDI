using NetCoreManualDI.Application.School.Dtos;

namespace NetCoreManualDI.Application.School
{
    public interface ISchoolService : IDisposable
    {
        Task<IReadOnlyList<CourseDto>> GetAllCourses();
        Task<IReadOnlyList<StudentDto>> GetAllStudents();
        Task<CourseDto> RegisterCourse(RegisterCourseDto dto);
        Task<StudentDto?> RegisterStudent(RegisterStudentDto dto);
        Task EnrollStudent(EnrollStudentDto dto);
    }
}