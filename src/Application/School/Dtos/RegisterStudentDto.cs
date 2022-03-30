using NetCoreManualDI.Application.Courses;
using NetCoreManualDI.Domain.Core.Courses;
using NetCoreManualDI.Domain.Core.Students;

namespace NetCoreManualDI.Application.School.Dtos
{
    public sealed record class RegisterStudentDto(string Name, string FavoriteCourseName)
    {
        public async Task<Student?> ToStudent(ICoursesRepository coursesRepository)
        {
            var favoriteCourse = await coursesRepository.GetByNameAsync(FavoriteCourseName.ToCourseName());
            if (favoriteCourse == null)
                return null;
            return new(Name.ToStudentName(), favoriteCourse);
        }
    }
}
