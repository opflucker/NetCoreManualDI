# .NET Core with Factory Based Dependency Injection
Example .NET Core applications (console and webapi), with onion architecture and manual dependency injection (DI). Instead of Pure DI technique (that suffers of poor decoupling), this solution uses lambda expressions as factories when resolving dependencies. This ensure the same level of decoupling compared to container based DI. 

## How it works

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

[ApplicationDomain](src/ApplicationDomain) project implements three interfaces. Implementations of [ICoursesService](src/ApplicationDomain/ICoursesService.cs) and [IStudentsService](src/ApplicationDomain/IStudentsService.cs) depends on [ISchoolContext](src/ApplicationDomain.Repositories/ISchoolContext.cs). This resource is created externally, so these implementations are not in charge of releasing it.

ApplicationDomain project also implements [ISchoolService](src/ApplicationDomain/ISchoolService.cs) in class [SchoolService](src/ApplicationDomain/SchoolService.cs). SchoolService depends on ISchoolContext and also needs to control its lifecycle, so it receives a factory. Additionally, SchoolService depends on ICoursesService and IStudentsService and must ensure all be created using the same ISchoolContext instance, so it receives factories for both too:

```C#
public SchoolService(
    Func<ISchoolContext> schoolContextFactory,
    Func<ISchoolContext, ICoursesService> coursesServiceFactory,
    Func<ISchoolContext, IStudentsService> studentsServiceFactory)
{
    schoolContext = schoolContextFactory();
    coursesService = coursesServiceFactory(schoolContext);
    studentsService = studentsServiceFactory(schoolContext);
}
```

SchoolService becomes owner of three resources (because it creates them) but only one is disposable (implements IDisposable), ISchoolContext, so it has to explicitly dispose it:

```C#
public void Dispose()
{
    schoolContext.Dispose();
    GC.SuppressFinalize(this);
}
```

In summary, if a class use a dependency, it receives an instance injected. If it also needs to control the dependency lifecycle, it receives a factory.

## In ASP.NET Core

Use DI factories in console is straighforward, as in project NetCoreManualDI.Console in file [Program.cs](src/Presentation.Console/Program.cs):

```C#
var schoolService = ApplicationDomainFactories.ForSchoolService(() => new SchoolContext(connectionString, true));
```

Use DI factories in ASP.NET Core requires to replace default web controllers activation. Fortunatelly, this is simple. Project NetCoreManualDI.WebApi shows it in file [Program.cs](src/Presentation.WebApi/Program.cs):

```C#
builder.Services
    .AddSingleton<IControllerActivator>(sp => new CustomControllerActivator(sp))
    .AddControllers();
```

Class [CustomControllerActivator](src/Presentation.WebApi/CustomControllerActivator.cs) must create controller instances passing all required parameters:

```C#
public object Create(ControllerContext context)
{
    if (context.ActionDescriptor.ControllerTypeInfo == typeof(SchoolController))
        return new SchoolController(
            loggerFactory.CreateLogger<SchoolController>(),
            schoolServiceFactory);

    throw new ArgumentException("Unkown controller", nameof(context));
}
```
