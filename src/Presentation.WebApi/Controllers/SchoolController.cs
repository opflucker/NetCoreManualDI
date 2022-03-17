using Microsoft.AspNetCore.Mvc;
using NetCoreManualDI.ApplicationDomain;
using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly ILogger<SchoolController> logger;
        private readonly Func<ISchoolService> schoolServiceFactory;

        public SchoolController(
            ILogger<SchoolController> logger,
            Func<ISchoolService> schoolServiceFactory)
        {
            this.logger = logger;
            this.schoolServiceFactory = schoolServiceFactory;
        }

        [HttpPost]
        public async Task<ActionResult> EnrollStudent(string studentName, string courseName)
        {
            logger.LogInformation("Calling EnrollStudent");

            using (var someService = schoolServiceFactory())
            {
                await someService.EnrollStudent(studentName.ToStudentName(), courseName.ToCourseName());
            }

            return Ok();
        }
    }
}