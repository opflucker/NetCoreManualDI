# .NET Core with Factory Based Dependency Injection
Example .NET Core applications (console and webapi), with onion architecture and manual dependency injection (DI). Instead of Pure DI technique (that suffers of poor decoupling), this solution uses factories when resolving dependencies. This ensure the same level of decoupling compared to container based DI.

## How it works
The key element is the injection of dependency factories (**d-factories**) when we want to control their lifecycles. In this example, some d-factories are defined in class [Factories](src/Application/Factories.cs):

```C#
public static class Factories
{
    private static Func<ISchoolContextWithEvents> CreateSchoolContextFactory(Func<ISchoolContext> contextFactory, Func<IEventsDispatcher> eventsDispatcherFactory)
        => () => new SchoolContextWithEvents(contextFactory, eventsDispatcherFactory);

    public static ISchoolService CreateSchoolService(Func<ISchoolContext> contextFactory, Func<IEventsDispatcher> eventsDispatcherFactory)
        => new SchoolService(CreateSchoolContextFactory(contextFactory, eventsDispatcherFactory));
}
```

This class defines some d-factories: `CreateSchoolContextFactory` and `CreateSchoolService`. Each one knows how to create an internal implemented interface. This technique is composable, so a d-factory can be defined using others d-factories, as in `CreateSchoolService`.
 
Class [SchoolService](src/Application/School/SchoolService.cs) depends on `ISchoolContextWithEvents` and needs to control its lifecycle, so it receives a d-factory:

```C#
public SchoolService(Func<ISchoolContextWithEvents> createContext)
{
    context = createContext();
}
```

`SchoolService` becomes owner of this resource that implements IDisposable, so it has to explicitly dispose it:

```C#
public void Dispose()
{
    context.Dispose();
    GC.SuppressFinalize(this);
}
```

In summary, if a class use a dependency, it receives an instance injected. If it also needs to control the dependency lifecycle, it receives a factory instead.

## In ASP.NET Core
Use d-factories in console is straighforward, as in project [NetCoreManualDI.Console](src/Presentation.Console) in file [Program.cs](src/Presentation.Console/Program.cs):

```C#
var schoolServiceFactory = () => NetCoreManualDI.Application.Factories.CreateSchoolService(schoolContextFactory, eventsDispatcherFactory);
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
