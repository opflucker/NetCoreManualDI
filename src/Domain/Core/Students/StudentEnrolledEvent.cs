using NetCoreManualDI.Domain.Commons;

namespace NetCoreManualDI.Domain.Core.Students
{
    public sealed record StudentEnrolledEvent(StudentName Name) : IEvent;
}
