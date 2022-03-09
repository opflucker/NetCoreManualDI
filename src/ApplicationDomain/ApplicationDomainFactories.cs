using NetCoreManualDI.ApplicationDomain.Repositories;

namespace NetCoreManualDI.ApplicationDomain
{
    public static class ApplicationDomainFactories
    {
        public static Func<ISchoolContext, ICoursesService> ForCoursesService
            => (context) => new CoursesService(context);

        public static Func<ISchoolContext, IStudentsService> ForStudentsService
            => (context) => new StudentsService(context);

        public static Func<Func<ISchoolContext>, ISchoolService> ForSchoolService
            => (contextFactory) => new SchoolService(contextFactory, ForCoursesService, ForStudentsService);
    }
}
