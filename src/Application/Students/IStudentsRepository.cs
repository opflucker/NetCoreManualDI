using NetCoreManualDI.Domain.Core.Students;

namespace NetCoreManualDI.Application.Students
{
    public interface IStudentsRepository
    {
        Task<IReadOnlyList<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(Guid id);
        Task<Student?> GetByNameAsync(StudentName name);
        void Register(Student student);
    }
}
