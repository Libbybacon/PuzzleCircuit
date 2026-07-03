namespace PuzzleCircuit.DAL.Entities;

public class PuzzleCompany {
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public ICollection<Puzzle> Puzzles { get; set; } = [];
}