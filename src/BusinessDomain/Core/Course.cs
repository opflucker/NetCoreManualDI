namespace NetCoreManualDI.BusinessDomain.Core
{
    public class Course
    {
        public Guid Id { get; }
        public string Name { get; set; }

        public Course(string name)
        {
            Name = name;
        }

        #pragma warning disable CS8618
        private Course() { }
        #pragma warning restore CS8618
    }
}