namespace TravelAndTours.Application.Auth.Models;

public sealed record MeResponse(
    int UserId,
    string Email,
    IList<string> Roles
);
