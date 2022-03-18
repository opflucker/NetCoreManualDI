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

// Use case: Initialize database
var schoolService = Factories.ForSchoolService(() => new SchoolContext(connectionString, true));
await schoolService.Initialize();

// Use case: enroll student
//var schoolService = ApplicationDomainFactories.ForSchoolService(() => new SchoolContext(connectionString, true));
await schoolService.EnrollStudent("Otto".ToStudentName(), "Math".ToCourseName());
//await schoolService.EnrollStudent("Otto".ToStudentName(), "Physics".ToCourseName());
//await schoolService.EnrollStudent("Otto".ToStudentName(), "History".ToCourseName());

// Use case: Read student
//using var schoolContext = new SchoolContext(connectionString, true);
//var student = await schoolContext.Students.GetByNameAsync("Otto".ToStudentName());
//Console.WriteLine(student);

// Use case: use an untracked object to set a relation
//Course? mathCourse;
//using (var schoolContext = new SchoolContext(connectionString, true))
//{
//    mathCourse = await schoolContext.Courses.GetByNameAsync("Math");
//}
//if (mathCourse != null)
//{
//    using (var schoolContext = new SchoolContext(connectionString, true))
//    {
//        var student = await schoolContext.Students.GetByNameAsync("Otto".ToStudentName());
//        if (student != null)
//        {
//            student.ChangeFavoriteCourse(mathCourse);
//            await schoolContext.SaveChangesAsync();
//        }
//    }
//}
