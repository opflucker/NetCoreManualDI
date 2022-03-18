using NetCoreManualDI.BusinessDomain.Commons;
using NetCoreManualDI.BusinessDomain.Core.Courses;
using NetCoreManualDI.BusinessDomain.Core.Students;

namespace NetCoreManualDI.BusinessDomain.Core.Students
{
    public class Enrollment : Entity
    {
        public Student Student { get; }
        public Course Course { get; }

        public Enrollment(Student student, Course course)
        {
            Student = student;
            Course = course;
        }

        #pragma warning disable CS8618
        private Enrollment() { }
        #pragma warning restore CS8618
    }
}