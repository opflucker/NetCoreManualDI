using NetCoreManualDI.ApplicationDomain.Courses;
using NetCoreManualDI.ApplicationDomain.Events;
using NetCoreManualDI.ApplicationDomain.School;
using NetCoreManualDI.ApplicationDomain.Students;

namespace NetCoreManualDI.ApplicationDomain
{
    public static class Factories
    {
        private static ICoursesService ForCoursesService(ICoursesRepository repository)
            => new CoursesService(repository);

        private static IStudentsService ForStudentsService(IStudentsRepository repository) 
            => new StudentsService(repository);

        private static Func<ISchoolContextWithEvents> ForSchoolContextWithEvents(Func<ISchoolContext> contextFactory, Func<IEventsDispatcher> eventsDispatcherFactory)
            => () => new SchoolContextWithEvents(contextFactory, eventsDispatcherFactory);

        public static ISchoolService ForSchoolService(Func<ISchoolContext> contextFactory, Func<IEventsDispatcher> eventsDispatcherFactory)
            => new SchoolService(ForSchoolContextWithEvents(contextFactory, eventsDispatcherFactory), ForCoursesService, ForStudentsService);
    }
}
