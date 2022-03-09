using Microsoft.Extensions.Configuration;
using NetCoreManualDI.ApplicationDomain;
using NetCoreManualDI.Persistence;

var connectionString = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build()
    .GetConnectionString("DefaultConnection");

var schoolService = ApplicationDomainFactories.ForSchoolService(() => new SchoolContext(connectionString, true));

await schoolService.EnrollStudent("Otto", "Math");
