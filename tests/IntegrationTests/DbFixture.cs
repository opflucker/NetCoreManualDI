using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NetCoreManualDI.Domain.Core.Courses;
using NetCoreManualDI.Domain.Core.Students;
using NetCoreManualDI.Persistence.Design;
using Polly;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreManualDI.IntegrationTests
{
    internal class DbFixture
    {
        public SchoolDbContext DbContext { get; }

        public DbFixture()
        {
            DbContext = BuildDbContext().GetAwaiter().GetResult();
        }

        private static async Task<SchoolDbContext> BuildDbContext()
        {
            var connectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("DefaultConnection");

            await EnsureCanConnectToDB(connectionString);

            var dbContext = new SchoolDbContext(connectionString, false);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            await Populate(dbContext);

            return dbContext;
        }

        private static async Task EnsureCanConnectToDB(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            connectionStringBuilder.InitialCatalog = "master";
            var masterConnectionString = connectionStringBuilder.ToString();

            await Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(10, retryCount => TimeSpan.FromSeconds(2))
                .ExecuteAsync(async () =>
                {
                    using (var sqlConnection = new SqlConnection(masterConnectionString))
                        await sqlConnection.OpenAsync();
                });
        }

        private static async Task Populate(SchoolDbContext dbContext)
        {
            var courseNames = new[] { "Math", "Physics", "History" };
            var courses = courseNames.Select(name => new Course(name.ToCourseName())).ToArray();
            foreach (var course in courses)
                dbContext.Courses.Add(course);

            var student = new Student("Otto".ToStudentName(), courses.First());
            dbContext.Students.Add(student);

            await dbContext.SaveChangesAsync();
        }
    }
}
