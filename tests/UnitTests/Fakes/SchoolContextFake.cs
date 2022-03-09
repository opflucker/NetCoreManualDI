using NetCoreManualDI.ApplicationDomain.Repositories;
using System.Threading.Tasks;

namespace NetCoreManualDI.UnitTests.Fakes
{
    internal class SchoolContextFake : ISchoolContext
    {
        public IStudentsRepository Students { get; }
        public ICoursesRepository Courses { get; }

        public SchoolContextFake()
        {
            Students = new StudentsRepositoryFake();
            Courses = new CoursesRepositoryFake();
        }

        public void Dispose()
        {
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
