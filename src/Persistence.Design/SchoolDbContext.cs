using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.Persistence.Design
{
    public sealed class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();

        private readonly string _connectionString;
        private readonly bool _useConsoleLogger;

        public SchoolDbContext(string connectionString, bool useConsoleLogger)
        {
            _connectionString = connectionString;
            _useConsoleLogger = useConsoleLogger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString);

            if (_useConsoleLogger)
            {
                optionsBuilder
                    .LogTo(Console.WriteLine, new string[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(b => 
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Name);
                b.HasOne(p => p.FavoriteCourse).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
                b.HasMany(p => p.Enrollments).WithOne(p => p.Student);
            });
                
            modelBuilder.Entity<Course>(b =>
            {
                b.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Enrollment>(b => 
            {
                b.HasKey(e => e.Id);
                b.HasOne(p => p.Student).WithMany(p => p.Enrollments);
                b.HasOne(p => p.Course).WithMany();
            });
        }
    }
}