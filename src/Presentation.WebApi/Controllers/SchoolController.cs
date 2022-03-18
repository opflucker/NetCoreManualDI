using Microsoft.AspNetCore.Mvc;
using NetCoreManualDI.ApplicationDomain.School;
using NetCoreManualDI.BusinessDomain.Core.Courses;
using NetCoreManualDI.BusinessDomain.Core.Students;

namespace NetCoreManualDI.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase, IDisposable
    {
        private readonly ILogger<SchoolController> logger;
        private readonly ISchoolService schoolService;

        public SchoolController(
            ILogger<SchoolController> logger,
            Func<ISchoolService> schoolServiceFactory)
        {
            this.logger = logger;
            schoolService = schoolServiceFactory();
        }

        public void Dispose()
        {
            schoolService.Dispose();
            GC.SuppressFinalize(this);
        }

        [HttpPost]
        public async Task<ActionResult> EnrollStudent(string studentName, string courseName)
        {
            logger.LogInformation("Calling EnrollStudent");

            await schoolService.EnrollStudent(studentName.ToStudentName(), courseName.ToCourseName());

            return Ok();
        }
    }
}