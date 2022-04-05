using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using NetCoreManualDI.Application.School;
using NetCoreManualDI.Persistence.Design;
using NetCoreManualDI.WebApi.Controllers;

namespace NetCoreManualDI.WebApi
{
    public class CustomControllerActivator : IControllerActivator
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger<CustomControllerActivator> logger;
        private readonly Func<ISchoolService> schoolServiceFactory;

        public CustomControllerActivator(IServiceProvider serviceProvider)
        {
            loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            logger = loggerFactory.CreateLogger<CustomControllerActivator>();

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            logger.LogInformation($"Using connectionString={connectionString}");

            using (var dbContext = new SchoolDbContext(connectionString, false))
            {
                logger.LogInformation($"Ensuring DB created...");
                dbContext.Database.EnsureCreatedAsync().GetAwaiter().GetResult();
                logger.LogInformation($"DB created");
            }

            var schoolContextFactory = () => Persistence.Factories.CreateSchoolContext(connectionString, true);
            var eventsDispatcherFactory = EventsDispatching.Factories.CreateEventsDispatcher;
            schoolServiceFactory = () => Application.Factories.CreateSchoolService(schoolContextFactory, eventsDispatcherFactory);
        }

        public object Create(ControllerContext context)
        {
            logger.LogInformation($"Creating controller={context.ActionDescriptor.ControllerTypeInfo.FullName}");

            if (context.ActionDescriptor.ControllerTypeInfo == typeof(SchoolController))
                return new SchoolController(
                    loggerFactory.CreateLogger<SchoolController>(),
                    schoolServiceFactory);

            throw new ArgumentException("Unkown controller", nameof(context));
        }

        public void Release(ControllerContext context, object controller)
        {
            logger.LogInformation($"Disposing controller={typeof(Controller).FullName}");

            if (controller is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
