using NetCoreManualDI.ApplicationDomain.Courses;
using NetCoreManualDI.ApplicationDomain.Events;
using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.ApplicationDomain.School;
using NetCoreManualDI.ApplicationDomain.Students;

namespace NetCoreManualDI.ApplicationDomain
{
    public static class Factories
    {
        private static Func<ISchoolContext, ICoursesService> ForCoursesService
            => (context) => new CoursesService(context);

        private static Func<ISchoolContext, IStudentsService> ForStudentsService
            => (context) => new StudentsService(context);

        private static IEventsDispatcher DefaultEventsDispatcher 
            = new ConsoleEventsDispatcher();

        private static Func<Func<ISchoolContext>, Func<ISchoolContextWithEvents>> ForSchoolContextWithEvents
            => (contextFactory) => () => new SchoolContextWithEvents(contextFactory, DefaultEventsDispatcher);

        public static Func<Func<ISchoolContext>, ISchoolService> ForSchoolService
            => (contextFactory) => new SchoolService(ForSchoolContextWithEvents(contextFactory), ForCoursesService, ForStudentsService);
    }
}
