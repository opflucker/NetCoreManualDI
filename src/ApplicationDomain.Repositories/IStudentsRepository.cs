using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.ApplicationDomain.Repositories
{
    public interface IStudentsRepository
    {
        Task<Student?> GetByIdAsync(Guid id);
        Task<Student?> GetByNameAsync(string name);
        void Register(Student student);
    }
}
