using PuzzleCircuit.DAL.Entities.Admin;
using PuzzleCircuit.DAL.Entities.Common;

namespace PuzzleCircuit.DAL.Entities;

public class EventRegistration : TrackingBase {
    public long RegistrationGroupId { get; set; }
    public RegistrationGroup RegistrationGroup { get; set; } = null!;

    public long EventId { get; set; }
    public CompetitionEvent Event { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;

    public DateTime RegisteredUtc { get; set; } = DateTime.UtcNow;
    public DateTime? CheckedInUtc { get; set; }

}
