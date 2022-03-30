using Microsoft.AspNetCore.Mvc;
using NetCoreManualDI.Application.School;
using NetCoreManualDI.Application.School.Dtos;

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

        [HttpGet("courses")]
        public async Task<ActionResult> GetAllCourses()
        {
            var courses = await schoolService.GetAllCourses();

            return Ok(courses);
        }

        [HttpPost("courses")]
        public async Task<ActionResult> RegisterCourse([FromBody] RegisterCourseDto dto)
        {
            logger.LogInformation($"Registering {dto}...");

            var courseDto = await schoolService.RegisterCourse(dto);

            return Ok(courseDto);
        }

        [HttpGet("students")]
        public async Task<ActionResult> GetAllStudents()
        {
            var students = await schoolService.GetAllStudents();

            return Ok(students);
        }

        [HttpPost("students")]
        public async Task<ActionResult> RegisterStudent([FromBody] RegisterStudentDto dto)
        {
            logger.LogInformation($"Registering {dto}...");

            var studentDto = await schoolService.RegisterStudent(dto);

            return Ok(studentDto);
        }

        [HttpPost("enrolls")]
        public async Task<ActionResult> EnrollStudent([FromBody] EnrollStudentDto dto)
        {
            logger.LogInformation($"Enrolling {dto}...");

            await schoolService.EnrollStudent(dto);

            return NoContent();
        }
    }
}