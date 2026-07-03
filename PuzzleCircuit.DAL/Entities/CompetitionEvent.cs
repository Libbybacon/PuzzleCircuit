using PuzzleCircuit.DAL.Entities.Common;

namespace PuzzleCircuit.DAL.Entities {
    public class CompetitionEvent : TrackingBase {
        public CompetitionEvent() { }

        public long CompetitionId { get; set; }
        public Competition Competition { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ParticipantType ParticipantType { get; set; }

        public int PieceCount { get; set; }

        public DateTime StartUtc { get; set; }
        public DateTime? EndUtc { get; set; }

        // Number of competing units, not number of individual users.
        public int? MaxEntries { get; set; }

        public EventStatus Status { get; set; } = EventStatus.Draft;

        public int SortOrder { get; set; }

        public ICollection<RegistrationGroup> RegistrationGroups { get; set; } = [];
    }
}
