using Microsoft.Extensions.Configuration;
using NetCoreManualDI.ApplicationDomain;
using NetCoreManualDI.BusinessDomain.Core.Courses;
using NetCoreManualDI.BusinessDomain.Core.Students;
using NetCoreManualDI.Persistence;

var connectionString = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build()
    .GetConnectionString("DefaultConnection");

var schoolContextFactory = () => new SchoolContext(connectionString, true);

using (var schoolService = Factories.ForSchoolService(schoolContextFactory))
{
    await schoolService.Initialize();
    await schoolService.EnrollStudent("Otto".ToStudentName(), "Physics".ToCourseName());
}

using (var schoolContext = schoolContextFactory())
{
    var student = await schoolContext.Students.GetByNameAsync("Otto".ToStudentName());
    if (student != null)
    {
        Console.WriteLine($"{student.Name}, with enrollments:");
        foreach(var e in student.Enrollments)
            Console.WriteLine(e.Course.Name);
    }
}
