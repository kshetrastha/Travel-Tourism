using System.Security.Claims;

namespace TravelAndTours.Application.Abstractions.Authentication;

public interface ICurrentUserService
{
    int? UserId { get; }
    string? Email { get; }
    IReadOnlyList<string> Roles { get; }
    bool IsAuthenticated { get; }
    ClaimsPrincipal Principal { get; }
}
