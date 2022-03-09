namespace NetCoreManualDI.ApplicationDomain
{
    public interface ISchoolService : IDisposable
    {
        Task Initialize();
        Task EnrollStudent(string studentName, string courseName);
    }
}