using PuzzleCircuit.DAL.Entities.Admin;
using PuzzleCircuit.DAL.Entities.Common;

namespace PuzzleCircuit.DAL.Entities;

public class RegistrationGroup : TrackingBase {
    public long EventId { get; set; }
    public CompetitionEvent Event { get; set; } = null!;

    public string? DisplayName { get; set; }

    // User who created the group / captain / main contact.
    public string CreatorUserId { get; set; } = null!;
    public AppUser CreatorUser { get; set; } = null!;

    public RegistrationGroupStatus Status { get; set; } = RegistrationGroupStatus.Active;

    public ICollection<EventRegistration> Registrations { get; set; } = [];
    public ICollection<EventResult> Results { get; set; } = [];
    public ICollection<AppUser> Contestants { get; set; } = [];
}
