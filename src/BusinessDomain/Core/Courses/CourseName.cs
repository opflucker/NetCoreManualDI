namespace NetCoreManualDI.BusinessDomain.Core.Courses
{
    public sealed record class CourseName(string Name) : NotNullOrWhiteSpaceText(Name);

    public static partial class String_Extensions
    {
        public static CourseName ToCourseName(this string name) => new CourseName(name);
    }
}
