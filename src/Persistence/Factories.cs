using NetCoreManualDI.ApplicationDomain.School;

namespace NetCoreManualDI.Persistence
{
    public static class Factories
    {
        public static ISchoolContext ForSchoolContext(string connectionString, bool useConsoleLogger)
            => new SchoolContext(connectionString, useConsoleLogger);
    }
}
