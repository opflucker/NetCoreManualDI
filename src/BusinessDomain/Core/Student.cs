namespace NetCoreManualDI.BusinessDomain.Core
{
    public sealed class Student
    {
        public Guid Id { get; }
        public string Name { get; }
        public Course FavoriteCourse { get; }

        private readonly List<Enrollment> _enrollments;
        public IReadOnlyList<Enrollment> Enrollments => _enrollments;

        public Student(string name, Course favoriteCourse)
        {
            Name = name;
            FavoriteCourse = favoriteCourse;
            _enrollments = new List<Enrollment>();
        }

        public void EnrollIn(Course course)
        {
            _enrollments.Add(new Enrollment(this, course));
        }

        #pragma warning disable CS8618
        private Student() { }
        #pragma warning restore CS8618
    }
}