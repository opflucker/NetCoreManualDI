using Microsoft.EntityFrameworkCore;
using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.Persistence
{
    internal sealed class StudentsRepository : IStudentsRepository
    {
        private readonly DbSet<Student> Students;

        public StudentsRepository(DbSet<Student> Students)
        {
            this.Students = Students;
        }

        public Task<Student?> GetByIdAsync(Guid id)
        {
            return Students
                .Include(s => s.FavoriteCourse)
                .Include(s => s.Enrollments)
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public Task<Student?> GetByNameAsync(string name)
        {
            return Students
                .Include(s => s.FavoriteCourse)
                .Include(s => s.Enrollments)
                .SingleOrDefaultAsync(s => s.Name == name);
        }

        public void Register(Student student)
        {
            Students.Add(student);
        }
    }
}
