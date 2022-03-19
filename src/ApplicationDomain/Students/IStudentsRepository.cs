using NetCoreManualDI.BusinessDomain.Core.Students;

namespace NetCoreManualDI.ApplicationDomain.Students
{
    public interface IStudentsRepository
    {
        Task<Student?> GetByIdAsync(Guid id);
        Task<Student?> GetByNameAsync(StudentName name);
        void Register(Student student);
    }
}
