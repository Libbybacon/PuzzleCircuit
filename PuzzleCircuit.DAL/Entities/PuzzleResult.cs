using PuzzleCircuit.DAL.Entities.Admin;
using PuzzleCircuit.DAL.Entities.Common;

namespace PuzzleCircuit.DAL.Entities;

public class PuzzleResult : TrackingBase {
    public PuzzleResult() { }

    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    public long PuzzleId { get; set; }
    public Puzzle Puzzle { get; set; } = default!;

    public DateTime DateCompleted { get; set; }
    public TimeSpan CompletionTime { get; set; }
    public int AttemptNumber { get; set; } = 1;

    public bool UsedReference { get; set; } = true;

}
