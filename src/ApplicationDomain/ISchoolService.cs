using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.ApplicationDomain
{
    public interface ISchoolService : IDisposable
    {
        Task Initialize();
        Task EnrollStudent(StudentName studentName, CourseName courseName);
    }
}