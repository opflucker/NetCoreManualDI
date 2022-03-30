using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using NetCoreManualDI.Application.School;
using NetCoreManualDI.WebApi.Controllers;

namespace NetCoreManualDI.WebApi
{
    public class CustomControllerActivator : IControllerActivator
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly Func<ISchoolService> schoolServiceFactory;

        public CustomControllerActivator(IServiceProvider serviceProvider)
        {
            loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var schoolContextFactory = () => Persistence.Factories.CreateSchoolContext(connectionString, true);
            var eventsDispatcherFactory = EventsDispatching.Factories.CreateEventsDispatcher;
            schoolServiceFactory = () => Application.Factories.CreateSchoolService(schoolContextFactory, eventsDispatcherFactory);
        }

        public object Create(ControllerContext context)
        {
            if (context.ActionDescriptor.ControllerTypeInfo == typeof(SchoolController))
                return new SchoolController(
                    loggerFactory.CreateLogger<SchoolController>(),
                    schoolServiceFactory);

            throw new ArgumentException("Unkown controller", nameof(context));
        }

        public void Release(ControllerContext context, object controller)
        {
            if (controller is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
