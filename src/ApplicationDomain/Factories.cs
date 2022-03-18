using NetCoreManualDI.ApplicationDomain.Courses;
using NetCoreManualDI.ApplicationDomain.Events;
using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.ApplicationDomain.School;
using NetCoreManualDI.ApplicationDomain.Students;

namespace NetCoreManualDI.ApplicationDomain
{
    public static class Factories
    {
        private static Func<ICoursesRepository, ICoursesService> ForCoursesService
            => (repository) => new CoursesService(repository);

        private static Func<IStudentsRepository, IStudentsService> ForStudentsService
            => (repository) => new StudentsService(repository);

        private static readonly Lazy<IEventsDispatcher> DefaultEventsDispatcher = new();

        private static Func<Func<ISchoolContext>, Func<ISchoolContextWithEvents>> ForSchoolContextWithEvents
            => (contextFactory) => () => new SchoolContextWithEvents(contextFactory, DefaultEventsDispatcher.Value);

        public static Func<Func<ISchoolContext>, ISchoolService> ForSchoolService
            => (contextFactory) => new SchoolService(ForSchoolContextWithEvents(contextFactory), ForCoursesService, ForStudentsService);
    }
}
