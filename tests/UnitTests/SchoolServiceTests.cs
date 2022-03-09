using NetCoreManualDI.ApplicationDomain;
using NetCoreManualDI.ApplicationDomain.Repositories;
using NetCoreManualDI.BusinessDomain.Core;
using NetCoreManualDI.UnitTests.Fakes;
using Xunit;

namespace NetCoreManualDI.UnitTests
{
    public class SchoolServiceTests
    {
        ISchoolContext schoolContext;
        ISchoolService schoolService;

        public SchoolServiceTests()
        {
            schoolContext = new SchoolContextFake();
            schoolService = ApplicationDomainFactories.ForSchoolService(() => schoolContext);
        }

        [Fact]
        public void When_Enroll_Then_Student_Success()
        {
            var course1 = new Course("Math");
            schoolContext.Courses.Register(course1);

            Student student1 = new Student("Jose", course1);
            schoolContext.Students.Register(student1);

            schoolService.EnrollStudent(student1.Name, course1.Name);

            Assert.Equal(1, student1.Enrollments.Count);
        }
    }
}