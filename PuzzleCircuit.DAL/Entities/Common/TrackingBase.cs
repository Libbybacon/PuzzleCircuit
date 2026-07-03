namespace PuzzleCircuit.DAL.Entities.Common;

public abstract class TrackingBase {
    public long Id { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedUtc { get; set; }
}
