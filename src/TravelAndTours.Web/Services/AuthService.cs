using System.IdentityModel.Tokens.Jwt;
using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public sealed class AuthService : IAuthService
{
    private readonly ApiClient _api;
    private readonly IHttpContextAccessor _http;

    public AuthService(ApiClient api, IHttpContextAccessor http)
    {
        _api = api;
        _http = http;
    }

    public async Task<AuthResponse?> LoginAsync(LoginViewModel vm, CancellationToken ct = default)
    {
        // Expected API endpoint: POST /api/auth/login
        var res = await _api.PostAsync<LoginViewModel, AuthResponse>("api/auth/login", vm, ct);
        if (res is null || string.IsNullOrWhiteSpace(res.AccessToken)) return null;

        var session = _http.HttpContext?.Session;
        if (session is null) return null;

        session.SetString(SessionKeys.AccessToken, res.AccessToken);
        if (!string.IsNullOrWhiteSpace(res.Email)) session.SetString(SessionKeys.UserEmail, res.Email);
        if (!string.IsNullOrWhiteSpace(res.UserName)) session.SetString(SessionKeys.UserName, res.UserName);

        if (res.Roles?.Length > 0)
        {
            session.SetString(SessionKeys.UserRoles, string.Join(",", res.Roles));
        }
        else
        {
            // Optional: try reading roles from JWT if API doesn't return roles
            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(res.AccessToken);
                var roles = jwt.Claims.Where(c => c.Type == "role" || c.Type.EndsWith("/claims/role")).Select(c => c.Value).Distinct().ToArray();
                if (roles.Length > 0)
                    session.SetString(SessionKeys.UserRoles, string.Join(",", roles));
            }
            catch { /* ignore */ }
        }

        return res;
    }

    public async Task RegisterAsync(RegisterViewModel vm, CancellationToken ct = default)
    {
        // Expected API endpoint: POST /api/auth/register
        await _api.PostAsync<RegisterViewModel, object>("api/auth/register", vm, ct);
    }

    public void SignOut()
    {
        var session = _http.HttpContext?.Session;
        session?.Remove(SessionKeys.AccessToken);
        session?.Remove(SessionKeys.UserEmail);
        session?.Remove(SessionKeys.UserName);
        session?.Remove(SessionKeys.UserRoles);
    }

    public bool IsSignedIn()
    {
        var token = _http.HttpContext?.Session.GetString(SessionKeys.AccessToken);
        return !string.IsNullOrWhiteSpace(token);
    }
}
