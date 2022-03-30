using NetCoreManualDI.Domain.Core.Courses;

namespace NetCoreManualDI.Application.School.Dtos
{
    public sealed record class CourseDto(string Name);

    public static class CourseExtensions
    {
        public static CourseDto ToDto(this Course course) => new CourseDto(course.Name.Name);
    }
}
