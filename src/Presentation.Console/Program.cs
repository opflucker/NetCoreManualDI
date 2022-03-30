using Microsoft.Extensions.Configuration;
using NetCoreManualDI.Application.School.Dtos;
using NetCoreManualDI.Domain.Core.Students;
using NetCoreManualDI.Persistence.Design;

var connectionString = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build()
    .GetConnectionString("DefaultConnection");

using (var schoolDbContext = new SchoolDbContext(connectionString, true))
{
    await schoolDbContext.Database.EnsureDeletedAsync();
    await schoolDbContext.Database.EnsureCreatedAsync();
}

var schoolContextFactory = () => NetCoreManualDI.Persistence.Factories.CreateSchoolContext(connectionString, true);
var eventsDispatcherFactory = NetCoreManualDI.EventsDispatching.Factories.CreateEventsDispatcher;
var schoolServiceFactory = () => NetCoreManualDI.Application.Factories.CreateSchoolService(schoolContextFactory, eventsDispatcherFactory);

using (var schoolService = schoolServiceFactory())
{
    var courseNames = new[] { "Math", "Physics", "History" };
    foreach (var name in courseNames)
        await schoolService.RegisterCourse(new RegisterCourseDto(name));

    await schoolService.RegisterStudent(new RegisterStudentDto("Otto", courseNames.First()));

    await schoolService.EnrollStudent(new EnrollStudentDto("Otto", "Physics"));
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
