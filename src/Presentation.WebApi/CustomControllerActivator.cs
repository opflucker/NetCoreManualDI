using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using NetCoreManualDI.ApplicationDomain;
using NetCoreManualDI.Persistence;
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
            var schoolContextFactory = () => new SchoolContext(connectionString, true);
            schoolServiceFactory = () => ApplicationDomainFactories.ForSchoolService(schoolContextFactory);
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
        }
    }
}
