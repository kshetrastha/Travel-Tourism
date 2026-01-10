namespace TravelAndTours.Application.Abstractions.Authentication;

public sealed record ApplicationUserInfo(
    int UserId,
    string Email,
    IList<string> Roles
);
