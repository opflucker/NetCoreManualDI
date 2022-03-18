using NetCoreManualDI.BusinessDomain.Commons;

namespace NetCoreManualDI.BusinessDomain.Core.Students
{
    public sealed record StudentEnrolledEvent(StudentName Name) : IEvent;
}
