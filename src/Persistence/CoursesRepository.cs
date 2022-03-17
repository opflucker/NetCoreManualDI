using Microsoft.EntityFrameworkCore;
using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.Persistence
{
    internal sealed class CoursesRepository : ICoursesRepository
    {
        private readonly DbSet<Course> Courses;

        public CoursesRepository(DbSet<Course> Courses)
        {
            this.Courses = Courses;
        }

        public ValueTask<Course?> GetByIdAsync(Guid id)
        {
            return Courses.FindAsync(id);
        }

        public Task<Course?> GetByNameAsync(CourseName name)
        {
            return Courses.SingleOrDefaultAsync(c => c.Name == name);
        }

        public void Register(Course course)
        {
            Courses.Add(course);
        }
    }
}
