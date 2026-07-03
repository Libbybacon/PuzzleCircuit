namespace PuzzleCircuit.API.Contracts;

public sealed class RegistrationRequest {
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string? Location { get; set; }

}

public sealed class CurrentUserResponse {
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
}
