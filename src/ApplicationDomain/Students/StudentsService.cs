using NetCoreManualDI.BusinessDomain.Core.Students;

namespace NetCoreManualDI.ApplicationDomain.Students
{
    internal sealed class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository students;

        public StudentsService(IStudentsRepository students)
        {
            this.students = students;
        }

        public Task<Student?> GetByNameAsync(StudentName name)
        {
            return students.GetByNameAsync(name);
        }
    }
}
