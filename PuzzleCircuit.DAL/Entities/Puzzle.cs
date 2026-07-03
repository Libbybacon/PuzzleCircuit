using PuzzleCircuit.DAL.Entities.Common;

namespace PuzzleCircuit.DAL.Entities;

public class Puzzle : TrackingBase {
    public Puzzle() { }

    public long PuzzleCompanyId { get; set; }
    public PuzzleCompany PuzzleCompany { get; set; } = null!;

    public string Title { get; set; } = null!;
    public int PieceCount { get; set; }
    public string? Description { get; set; }
    public string? EAN { get; set; }
    public string? ManufacturerCode { get; set; }
    public string? ArtistName { get; set; }
    public int? Difficulty { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public PuzzleCut? PuzzleCut { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<PuzzleResult> PuzzleResults { get; set; } = [];

}
