using Microsoft.Extensions.Configuration;
using NetCoreManualDI.BusinessDomain.Core.Courses;
using NetCoreManualDI.BusinessDomain.Core.Students;

var connectionString = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build()
    .GetConnectionString("DefaultConnection");

var schoolContextFactory = () => NetCoreManualDI.Persistence.Factories.ForSchoolContext(connectionString, true);
var eventsDispatcherFactory = NetCoreManualDI.EventsDispatching.Factories.ForEventsDispatcher;
var schoolServiceFactory = () => NetCoreManualDI.ApplicationDomain.Factories.ForSchoolService(schoolContextFactory, eventsDispatcherFactory);

using (var schoolService = schoolServiceFactory())
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
