# .NET Core with Manual Dependency Injection
Experimental .NET Core applications (console and webapi), with onion architecture and manual dependency injection (DI). Instead of Poor's Man DI technique, this solution uses lambda expressions as factories for resolving dependencies. This technique ensure the same level of decoupling compared to container-based DI. 

The key element in this experiment is class [ApplicationDomainFactories](src/ApplicationDomain/ApplicationDomainFactories.cs):

```C#
    public static class ApplicationDomainFactories
    {
        public static Func<ISchoolContext, ICoursesService> ForCoursesService
            => (context) => new CoursesService(context);

        public static Func<ISchoolContext, IStudentsService> ForStudentsService
            => (context) => new StudentsService(context);

        public static Func<Func<ISchoolContext>, ISchoolService> ForSchoolService
            => (contextFactory) => new SchoolService(contextFactory, ForCoursesService, ForStudentsService);
    }
```
