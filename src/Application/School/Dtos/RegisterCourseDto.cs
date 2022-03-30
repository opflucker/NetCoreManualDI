using NetCoreManualDI.Domain.Core.Courses;

namespace NetCoreManualDI.Application.School.Dtos
{
    public sealed record class RegisterCourseDto(string Name)
    {
        public Course ToCourse() => new(Name.ToCourseName());
    }
}
