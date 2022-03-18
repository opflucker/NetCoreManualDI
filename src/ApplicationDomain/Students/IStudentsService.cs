using NetCoreManualDI.BusinessDomain.Core.Students;

namespace NetCoreManualDI.ApplicationDomain.Students
{
    public interface IStudentsService
    {
        Task<Student?> GetByNameAsync(StudentName name);
    }
}