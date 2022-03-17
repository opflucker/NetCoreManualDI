using NetCoreManualDI.BusinessDomain.Commons;

namespace NetCoreManualDI.BusinessDomain.Core
{
    public class Course : RootAggregate
    {
        public CourseName Name { get; set; }

        public Course(CourseName name)
        {
            Name = name;
        }

#pragma warning disable CS8618
        private Course() { }
#pragma warning restore CS8618
    }
}