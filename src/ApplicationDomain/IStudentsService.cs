using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.ApplicationDomain
{
    public interface IStudentsService
    {
        Task<Student?> GetByNameAsync(string name);
    }
}