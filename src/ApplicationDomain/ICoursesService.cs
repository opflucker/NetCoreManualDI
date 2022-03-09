using NetCoreManualDI.BusinessDomain.Core;

namespace NetCoreManualDI.ApplicationDomain
{
    public interface ICoursesService
    {
        Task<Course?> GetByNameAsync(string name);
    }
}