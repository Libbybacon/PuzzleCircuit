using PuzzleCircuit.DAL.Entities.Admin;

namespace PuzzleCircuit.DAL.Entities;

public class HostOrganization {
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public string AdminUserId { get; set; } = null!;
    public AppUser AdminUser { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public ICollection<HostLicense> Licenses { get; set; } = [];
    public ICollection<Competition> Competitions { get; set; } = [];
    public ICollection<AppUser> OrgUsers { get; set; } = [];
}
