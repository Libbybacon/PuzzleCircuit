using PuzzleCircuit.DAL.Entities.Common;

namespace PuzzleCircuit.DAL.Entities;

public class HostLicense : TrackingBase {
    public long HostId { get; set; }
    public HostOrganization HostOrganization { get; set; } = null!;

    public string LicenseKey { get; set; } = null!;
    public HostLicenseType LicenseType { get; set; }

    public DateTime StartUtc { get; set; }
    public DateTime ExpirationUtc { get; set; }

    public bool IsActive { get; set; } = true;
}