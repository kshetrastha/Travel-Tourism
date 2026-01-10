namespace TravelAndTours.Application.Auth.Models;

public sealed record AuthResponse(
    string Token,
    DateTime ExpiresAt,
    int UserId,
    string Email,
    IList<string> Roles
);
