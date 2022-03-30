using NetCoreManualDI.Domain.Commons;
using NetCoreManualDI.Domain.Core.Courses;

namespace NetCoreManualDI.Domain.Core.Students
{
    public sealed class Student : RootAggregate
    {
        public StudentName Name { get; }
        public Course FavoriteCourse { get; private set; }

        private readonly List<Enrollment> _enrollments;
        public IReadOnlyList<Enrollment> Enrollments => _enrollments;

        public Student(StudentName name, Course favoriteCourse)
        {
            Name = name;
            FavoriteCourse = favoriteCourse;
            _enrollments = new List<Enrollment>();
        }

        public void EnrollIn(Course course)
        {
            _enrollments.Add(new Enrollment(this, course));
            RaiseEvent(new StudentEnrolledEvent(Name));
        }

        public void ChangeFavoriteCourse(Course newFavoriteCourse)
        {
            FavoriteCourse = newFavoriteCourse;
        }

#pragma warning disable CS8618
        private Student() { }
#pragma warning restore CS8618
    }
}