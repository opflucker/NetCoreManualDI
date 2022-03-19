using NetCoreManualDI.ApplicationDomain;
using NetCoreManualDI.ApplicationDomain.Events;
using NetCoreManualDI.ApplicationDomain.School;
using NetCoreManualDI.BusinessDomain.Core.Courses;
using NetCoreManualDI.BusinessDomain.Core.Students;
using NetCoreManualDI.UnitTests.Fakes;
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
            schoolService = Factories.ForSchoolService(() => schoolContext, () => eventsDispatcher);
        }

        [Fact]
        public void When_Enroll_Then_Student_Success()
        {
            var course1 = new Course("Math".ToCourseName());
            schoolContext.Courses.Register(course1);

            Student student1 = new Student("Jose".ToStudentName(), course1);
            schoolContext.Students.Register(student1);

            schoolService.EnrollStudent(student1.Name, course1.Name);

            Assert.Equal(1, student1.Enrollments.Count);
        }
    }
}