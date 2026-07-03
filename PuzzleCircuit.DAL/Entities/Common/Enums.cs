namespace PuzzleCircuit.DAL.Entities.Common;

public enum ParticipantType
{
    Solo = 1,
    Pair = 2,
    Team = 3
}

public enum CompetitionStatus
{
    Draft = 1,
    Published = 2,
    RegistrationOpen = 3,
    RegistrationClosed = 4,
    InProgress = 5,
    Completed = 6,
    Cancelled = 7
}

public enum EventStatus
{
    Draft = 1,
    Open = 2,
    Closed = 3,
    InProgress = 4,
    Completed = 5,
    Cancelled = 6
}

public enum RegistrationStatus
{
    Pending = 1,
    Registered = 2,
    Waitlisted = 3,
    Cancelled = 4,
    CheckedIn = 5
}

public enum RegistrationGroupStatus
{
    Active = 1,
    Cancelled = 2,
    Disqualified = 3
}

public enum ResultStatus
{
    Pending = 1,
    Recorded = 2,
    Corrected = 3,
    Finalized = 4,
    Dnf = 5,
    Disqualified = 6
}

public enum HostLicenseType
{
    Monthly = 1,
    Annual = 2,
    PerEvent = 3,
    Manual = 4
}

public enum PuzzleCut
{
    Ribbon,
    RandomRibbon,
    Random
}