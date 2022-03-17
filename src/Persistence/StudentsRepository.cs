using Microsoft.EntityFrameworkCore;
using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Core;
using NetCoreManualDI.Persistence.Design;

namespace NetCoreManualDI.Persistence
{
    internal sealed class StudentsRepository : IStudentsRepository
    {
        private readonly SchoolDbContext context;

        public StudentsRepository(SchoolDbContext context)
        {
            this.context = context;
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            var student = await context.Students.FindAsync(id);

            if (student != null)
            {
                await Task.WhenAll(
                    context.Entry(student)
                        .Reference(s => s.FavoriteCourse)
                        .LoadAsync(),
                    context.Entry(student)
                        .Collection(s => s.Enrollments)
                        .Query()
                        .Include(e => e.Course)
                        .LoadAsync());
            }

            return student;
        }

        public async Task<Student?> GetByNameAsync(StudentName name)
        {
            var student = await context.Students
                .Include(s => s.FavoriteCourse)
                .SingleOrDefaultAsync(s => s.Name == name);

            if (student != null)
            {
                await context.Entry(student)
                    .Collection(s => s.Enrollments)
                    .Query()
                    .Include(e => e.Course)
                    .LoadAsync();
            }

            return student;
        }

        public void Register(Student student)
        {
            context.Students.Add(student);
        }
    }
}
