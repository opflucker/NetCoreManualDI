using Microsoft.Extensions.Configuration;
using NetCoreManualDI.BusinessDomain.Core.Courses;
using NetCoreManualDI.BusinessDomain.Core.Students;
using NetCoreManualDI.Persistence;
using ApplicationDomainFactories = NetCoreManualDI.ApplicationDomain.Factories;
using EventsDispatchingFactories = NetCoreManualDI.EventsDispatching.Factories;

var connectionString = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build()
    .GetConnectionString("DefaultConnection");

var schoolContextFactory = () => new SchoolContext(connectionString, true);

using (var schoolService = ApplicationDomainFactories.ForSchoolService(schoolContextFactory, EventsDispatchingFactories.ForEventsDispatcher))
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
