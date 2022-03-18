using NetCoreManualDI.BusinessDomain.Core.Courses;
using NetCoreManualDI.BusinessDomain.Core.Students;

namespace NetCoreManualDI.ApplicationDomain.School
{
    public interface ISchoolService : IDisposable
    {
        Task Initialize();
        Task EnrollStudent(StudentName studentName, CourseName courseName);
    }
}