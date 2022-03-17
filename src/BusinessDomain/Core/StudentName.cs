namespace NetCoreManualDI.BusinessDomain.Core
{
    public sealed record class StudentName(string Name) : NotNullOrWhiteSpaceText(Name);

    public static partial class String_Extensions
    {
        public static StudentName ToStudentName(this string name) => new StudentName(name);
    }
}
