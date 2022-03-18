﻿using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Commons;
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

        public async Task<IReadOnlyList<RootAggregate>> SaveChangesAsync()
        {
            var savedAggregates = context.ChangeTracker.Entries()
                .Where(e => e.Entity is RootAggregate)
                .Select(e => e.Entity as RootAggregate)
                .ToList();

            await context.SaveChangesAsync();

            return savedAggregates!;
        }
    }
}
