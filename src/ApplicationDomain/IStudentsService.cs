using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.ApplicationDomain
{
    public interface IStudentsService
    {
        Task<Student?> GetByNameAsync(StudentName name);
    }
}