namespace PuzzleCircuit.API {
    public static class ErrorCodes {
        public static readonly ErrorResponse EMAIL_IN_USE = new() {
            Reason = "EMAIL_ALREADY_IN_USE",
            Message = "An account with that email already exists."
        };

        public static readonly ErrorResponse EMAIL_NOT_IN_USE = new() {
            Reason = "EMAIL_NOT_IN_USE",
            Message = "No account with that email exists."
        };
    }

    public class ErrorResponse {
        public string Reason { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
