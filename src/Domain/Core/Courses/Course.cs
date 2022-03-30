using NetCoreManualDI.Domain.Commons;

namespace NetCoreManualDI.Domain.Core.Courses
{
    public class Course : RootAggregate
    {
        public CourseName Name { get; }

        public Course(CourseName name)
        {
            Name = name;
        }

#pragma warning disable CS8618
        private Course() { }
#pragma warning restore CS8618
    }
}