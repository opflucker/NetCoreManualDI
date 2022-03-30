using NetCoreManualDI.Application.School.Context;

namespace NetCoreManualDI.Persistence
{
    public static class Factories
    {
        public static ISchoolContext CreateSchoolContext(string connectionString, bool useConsoleLogger)
            => new SchoolContext(connectionString, useConsoleLogger);
    }
}
