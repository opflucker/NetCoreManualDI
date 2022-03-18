using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Core.Students;

namespace NetCoreManualDI.ApplicationDomain.Students
{
    internal sealed class StudentsService : IStudentsService
    {
        private readonly ISchoolContext schoolContext;

        public StudentsService(ISchoolContext schoolContext)
        {
            this.schoolContext = schoolContext;
        }

        public Task<Student?> GetByNameAsync(StudentName name)
        {
            return schoolContext.Students.GetByNameAsync(name);
        }
    }
}
