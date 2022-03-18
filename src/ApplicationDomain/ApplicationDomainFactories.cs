using NetCoreManualDI.ApplicationDomain.Repositories;

namespace NetCoreManualDI.ApplicationDomain
{
    public static class ApplicationDomainFactories
    {
        private static Func<ISchoolContext, ICoursesService> ForCoursesService
            => (context) => new CoursesService(context);

        private static Func<ISchoolContext, IStudentsService> ForStudentsService
            => (context) => new StudentsService(context);

        private static IBusinessDomainEventsDispatcher DefaultBusinessDomainEventsDispatcher 
            = new BusinessDomainEventsDispatcher();

        private static Func<Func<ISchoolContext>, Func<IApplicationContext>> ForApplicationContext
            => (contextFactory) => () => new ApplicationContext(contextFactory, DefaultBusinessDomainEventsDispatcher);

        public static Func<Func<ISchoolContext>, ISchoolService> ForSchoolService
            => (contextFactory) => new SchoolService(ForApplicationContext(contextFactory), ForCoursesService, ForStudentsService);
    }
}
