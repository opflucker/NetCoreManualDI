using NetCoreManualDI.Application;
using NetCoreManualDI.Application.Events;
using NetCoreManualDI.Application.School;
using NetCoreManualDI.Application.School.Context;
using NetCoreManualDI.Application.School.Dtos;
using NetCoreManualDI.Domain.Core.Students;
using NetCoreManualDI.UnitTests.Fakes;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreManualDI.UnitTests
{
    public class SchoolServiceTests
    {
        ISchoolContext schoolContext;
        IEventsDispatcher eventsDispatcher;
        ISchoolService schoolService;

        public SchoolServiceTests()
        {
            schoolContext = new SchoolContextFake();
            eventsDispatcher = new EventsDispatcherFake();
            schoolService = Factories.CreateSchoolService(() => schoolContext, () => eventsDispatcher);
        }

        [Fact]
        public async Task When_Enroll_Student_Then_Success()
        {
            var registerCourseDto1 = new RegisterCourseDto("Math");
            await schoolService.RegisterCourse(registerCourseDto1);

            var registerCourseDto2 = new RegisterCourseDto("Physics");
            await schoolService.RegisterCourse(registerCourseDto2);

            var registerStudentDto = new RegisterStudentDto("Jose", registerCourseDto1.Name);
            await schoolService.RegisterStudent(registerStudentDto);

            await schoolService.EnrollStudent(new EnrollStudentDto(registerStudentDto.Name, registerCourseDto2.Name));

            var student = await schoolContext.Students.GetByNameAsync(registerStudentDto.Name.ToStudentName());
            Assert.NotNull(student);
            Assert.Equal(1, student!.Enrollments.Count);
        }
    }
}