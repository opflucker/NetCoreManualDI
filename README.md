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

ApplicationDomain project implements some interfaces. [ICoursesService](src/ApplicationDomain.Repositories/ICoursesService.cs) and [IStudentsService](src/ApplicationDomain.Repositories/IStudentsService.cs) needs a [ISchoolContext](src/ApplicationDomain.Repositories/ISchoolContext.cs) object. This project does not know how to create this resource (because an onion architecture constraint), so it must be provided from outside. Additionally, because these implementations receive a resource created externally, they are not in charge of releasing it.

ApplicationDomain project also implements [ISchoolService](src/ApplicationDomain.Repositories/ISchoolService.cs) in class [SchoolService](src/ApplicationDomain/SchoolService.cs). In this case, this class needs to use instances of [ICoursesService](src/ApplicationDomain.Repositories/ICoursesService.cs) and [IStudentsService](src/ApplicationDomain.Repositories/IStudentsService.cs), and ensure all use the same instance of [ISchoolContext](src/ApplicationDomain.Repositories/ISchoolContext.cs). Additionally, this context , [SchoolService](src/ApplicationDomain/SchoolService.cs) needs to create 
