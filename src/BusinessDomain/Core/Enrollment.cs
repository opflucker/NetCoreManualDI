namespace NetCoreManualDI.BusinessDomain.Core
{
    public class Enrollment
    {
        public Guid Id { get; }
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