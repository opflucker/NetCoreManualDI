using NetCoreManualDI.BusinessDomain.Commons;

namespace NetCoreManualDI.BusinessDomain.Core
{
    public sealed record StudentEnrolledEvent(StudentName Name) : IEvent;
}
