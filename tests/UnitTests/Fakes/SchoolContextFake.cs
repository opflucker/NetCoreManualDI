using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Commons;
using System;
using System.Collections.Generic;
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

        public Task<IReadOnlyList<RootAggregate>> SaveChangesAsync()
        {
            return Task.FromResult((IReadOnlyList<RootAggregate>)Array.Empty<RootAggregate>());
        }
    }
}
