using System.Security.Claims;
using TravelAndTours.Application.Abstractions.Authentication;

namespace TravelAndTours.Api.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _http;

    public CurrentUserService(IHttpContextAccessor http)
    {
        _http = http;
    }

    public ClaimsPrincipal Principal => _http.HttpContext?.User ?? new ClaimsPrincipal(new ClaimsIdentity());

    public bool IsAuthenticated => Principal.Identity?.IsAuthenticated ?? false;

    public int? UserId
    {
        get
        {
            var value = Principal.FindFirstValue(ClaimTypes.NameIdentifier) ??
                        Principal.FindFirstValue("sub");

            if (int.TryParse(value, out var id))
                return id;

            return null;
        }
    }

    public string? Email => Principal.FindFirstValue(ClaimTypes.Email);

    public IReadOnlyList<string> Roles =>
        Principal.FindAll(ClaimTypes.Role).Select(c => c.Value).Distinct().ToList();
}
