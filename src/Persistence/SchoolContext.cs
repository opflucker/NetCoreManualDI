using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.Persistence.Design;

namespace NetCoreManualDI.Persistence
{
    public sealed class SchoolContext : ISchoolContext
    {
        private readonly SchoolDbContext context;

        public IStudentsRepository Students { get; }
        public ICoursesRepository Courses { get; }

        public SchoolContext(string connectionString, bool useConsoleLogger)
        {
            context = new SchoolDbContext(connectionString, useConsoleLogger);
            Students = new StudentsRepository(context);
            Courses = new CoursesRepository(context.Courses);
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task SaveChangesAsync() => context.SaveChangesAsync();
    }
}
