# .NET Core with Factory Based Dependency Injection
Example .NET Core applications (console and webapi), with onion architecture and manual dependency injection (DI). Instead of Pure DI technique (that suffers of poor decoupling), this solution uses factories when resolving dependencies. This ensure the same level of decoupling compared to container based DI.

## How it works
The key element is the injection of dependency factories (**d-factories**) when we want to control their lifecycles. In this example, some d-factories are defined in class [Factories](src/ApplicationDomain/Factories.cs):

```C#
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
```

This class defines some d-factories: `ForCoursesService`, `ForStudentsService`, `ForSchoolContextWithEvents` and `ForSchoolService`. Each one knows how to create an internal implemented interface. When any dependency can not be resolved at this level, a d-factory takes the form of a Partial Function Application ([PFA](https://en.wikipedia.org/wiki/Partial_application)), this is, a lambda expression that reduces the original function arity completing known parameters and exposing unknown ones. This technique is composable, so a d-factory can be defined using others d-factories, as in `ForSchoolService`.
 
[CoursesService](src/ApplicationDomain/Courses/CoursesService.cs) implements [ICoursesService](src/ApplicationDomain/Courses/ICoursesService.cs) and depends on [ICoursesRepository](src/ApplicationDomain.Repositories/ICoursesRepository.cs), but the resource is created externally, so `CoursesService` is not in charge of releasing it. Something similar happens with implementation of [StudentsService](src/ApplicationDomain/Students/StudentsService.cs).

[SchoolService](src/ApplicationDomain/School/SchoolService.cs) implements [ISchoolService](src/ApplicationDomain/School/ISchoolService.cs) and depends on `ISchoolContext`. `SchoolService` needs to control `ISchoolContext` lifecycle, so it receives a d-factory. Additionally, `SchoolService` depends on `ICoursesService` and `IStudentsService` and must ensure all be created using the same `ISchoolContext` instance, so it receives factories for both too:

```C#
public SchoolService(
    Func<ISchoolContextWithEvents> schoolContextFactory,
    Func<ICoursesRepository, ICoursesService> coursesServiceFactory,
    Func<IStudentsRepository, IStudentsService> studentsServiceFactory)
{
    schoolContext = schoolContextFactory();
    coursesService = coursesServiceFactory(schoolContext.School.Courses);
    studentsService = studentsServiceFactory(schoolContext.School.Students);
}
```

`SchoolService` becomes owner of three resources (because it creates them) but only one is disposable, so it has to explicitly dispose it:

```C#
public void Dispose()
{
    schoolContext.Dispose();
    GC.SuppressFinalize(this);
}
```

In summary, if a class use a dependency, it receives an instance injected. If it also needs to control the dependency lifecycle, it receives a factory instead. If it need to share a resource with a dependency, it receives a PFA d-factory and inject the resource when creating the dependency.

## In ASP.NET Core
Use d-factories in console is straighforward, as in project [NetCoreManualDI.Console](src/Presentation.Console) in file [Program.cs](src/Presentation.Console/Program.cs):

```C#
var schoolServiceFactory = () => NetCoreManualDI.ApplicationDomain.Factories.ForSchoolService(schoolContextFactory, eventsDispatcherFactory);
```

Use d-factories in ASP.NET Core requires to replace default web controllers activation. Fortunatelly, this is simple. Project [NetCoreManualDI.WebApi](src/Presentation.WebApi) shows it in file [Program.cs](src/Presentation.WebApi/Program.cs):

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
