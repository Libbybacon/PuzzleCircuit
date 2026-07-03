namespace PuzzleCircuit.DAL.Entities.Common;

public class Address : TrackingBase
{
    public string? AddressName { get; set; }
    public string? ContactName { get; set; }
    public string? StreetAddress1 { get; set; }
    public string? StreetAddress2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}