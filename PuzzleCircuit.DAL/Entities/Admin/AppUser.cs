using Microsoft.AspNetCore.Identity;

namespace PuzzleCircuit.DAL.Entities.Admin {
    public class AppUser : IdentityUser {
        public string DisplayName { get; set; } = null!;
        public string? Location { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<HostOrganization> HostOrganizations { get; set; } = [];
        public ICollection<EventRegistration> EventRegistrations { get; set; } = [];
        public ICollection<PuzzleResult> PuzzleResults { get; set; } = [];
        public ICollection<EventResult> ScorekeepingResults { get; set; } = [];
    }
}
