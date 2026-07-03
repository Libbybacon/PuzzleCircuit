using PuzzleCircuit.DAL.Entities.Common;

namespace PuzzleCircuit.DAL.Entities {
    public class Competition : Address {
        public long HostOrganizationId { get; set; }
        public HostOrganization HostOrganization { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Location { get; set; }

        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }

        public DateTime? RegistrationOpenUtc { get; set; }
        public DateTime? RegistrationCloseUtc { get; set; }

        public CompetitionStatus Status { get; set; } = CompetitionStatus.Draft;

        public ICollection<CompetitionEvent> Events { get; set; } = [];
    }
}
