using NetCoreManualDI.Application.Courses;
using NetCoreManualDI.Application.School.Context;
using NetCoreManualDI.Application.Students;
using NetCoreManualDI.Domain.Commons;
using NetCoreManualDI.Persistence.Design;

namespace NetCoreManualDI.Persistence
{
    internal sealed class SchoolContext : ISchoolContext
    {
        private readonly SchoolDbContext dbContext;

        public IStudentsRepository Students { get; }
        public ICoursesRepository Courses { get; }

        public SchoolContext(string connectionString, bool useConsoleLogger)
        {
            dbContext = new SchoolDbContext(connectionString, useConsoleLogger);
            Students = new StudentsRepository(dbContext);
            Courses = new CoursesRepository(dbContext.Courses);
        }

        public void Dispose()
        {
            dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IReadOnlyList<RootAggregate>> SaveChangesAsync()
        {
            var savedAggregates = dbContext.ChangeTracker.Entries()
                .Where(e => e.Entity is RootAggregate)
                .Select(e => e.Entity as RootAggregate)
                .ToList();

            await dbContext.SaveChangesAsync();

            return savedAggregates!;
        }
    }
}
