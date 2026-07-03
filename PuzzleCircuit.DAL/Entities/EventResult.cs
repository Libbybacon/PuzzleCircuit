using PuzzleCircuit.DAL.Entities.Admin;
using PuzzleCircuit.DAL.Entities.Common;

namespace PuzzleCircuit.DAL.Entities;

public class EventResult : TrackingBase {
    public long EventId { get; set; }
    public CompetitionEvent Event { get; set; } = null!;

    public long RegistrationGroupId { get; set; }
    public RegistrationGroup RegistrationGroup { get; set; } = null!;

    public long PuzzleId { get; set; }
    public Puzzle Puzzle { get; set; } = null!;

    // User who entered or updated the time (judge/host/staff).
    public string ScorekeeperId { get; set; } = string.Empty;
    public AppUser Scorekeeper { get; set; } = null!;

    public TimeSpan? RawTime { get; set; }
    public TimeSpan? PenaltyTime { get; set; }
    public TimeSpan? FinalTime { get; set; }
    public int? PiecesRemaining { get; set; }

    public ResultStatus Status { get; set; } = ResultStatus.Pending;

    public int? Placement { get; set; }

    public DateTime EnteredUtc { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedUtc { get; set; }
}
