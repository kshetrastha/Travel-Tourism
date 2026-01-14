using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        if (res is null || string.IsNullOrWhiteSpace(res.Token)) return null;

        var session = _http.HttpContext?.Session;
        if (session is null) return null;

        var resolvedRoles = ResolveRoles(res);
        res.Roles = resolvedRoles;

        session.SetString(SessionKeys.AccessToken, res.Token);
        if (!string.IsNullOrWhiteSpace(res.Email)) session.SetString(SessionKeys.UserEmail, res.Email);
        if (!string.IsNullOrWhiteSpace(res.UserName)) session.SetString(SessionKeys.UserName, res.UserName);

        if (resolvedRoles.Length > 0)
        {
            session.SetString(SessionKeys.UserRoles, string.Join(",", resolvedRoles));
        }

        return res;
    }

    public async Task SignInAsync(AuthResponse res, bool isPersistent, CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();
        var httpContext = _http.HttpContext;
        if (httpContext is null) return;

        var resolvedRoles = ResolveRoles(res);
        res.Roles = resolvedRoles;

        var claims = new List<Claim>();

        if (res.UserId > 0)
            claims.Add(new Claim(ClaimTypes.NameIdentifier, res.UserId.ToString()));
        if (!string.IsNullOrWhiteSpace(res.UserName))
            claims.Add(new Claim(ClaimTypes.Name, res.UserName));
        if (!string.IsNullOrWhiteSpace(res.Email))
            claims.Add(new Claim(ClaimTypes.Email, res.Email));

        foreach (var role in resolvedRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        if (claims.Count == 0 && !string.IsNullOrWhiteSpace(res.Email))
        {
            claims.Add(new Claim(ClaimTypes.Name, res.Email));
        }

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var properties = new AuthenticationProperties
        {
            IsPersistent = isPersistent,
            AllowRefresh = true
        };

        if (res.ExpiresAt.HasValue)
        {
            properties.ExpiresUtc = res.ExpiresAt.Value.ToUniversalTime();
        }

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            properties);
    }

    public async Task RegisterAsync(RegisterViewModel vm, CancellationToken ct = default)
    {
        // Expected API endpoint: POST /api/auth/register
        await _api.PostAsync<RegisterViewModel, object>("api/auth/register", vm, ct);
    }

    public async Task SignOutAsync(CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();
        var httpContext = _http.HttpContext;
        var session = httpContext?.Session;
        session?.Remove(SessionKeys.AccessToken);
        session?.Remove(SessionKeys.UserEmail);
        session?.Remove(SessionKeys.UserName);
        session?.Remove(SessionKeys.UserRoles);

        if (httpContext is not null)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }

    public bool IsSignedIn()
    {
        var httpContext = _http.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated == true)
            return true;

        var token = httpContext?.Session.GetString(SessionKeys.AccessToken);
        return !string.IsNullOrWhiteSpace(token);
    }

    private static string[] ResolveRoles(AuthResponse res)
    {
        var roles = NormalizeRoles(res.Roles);
        if (roles.Length > 0)
            return roles;

        if (string.IsNullOrWhiteSpace(res.Token))
            return Array.Empty<string>();

        try
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(res.Token);
            var jwtRoles = jwt.Claims
                .Where(c => c.Type == "role" || c.Type.EndsWith("/claims/role"))
                .Select(c => c.Value);
            return NormalizeRoles(jwtRoles);
        }
        catch
        {
            return Array.Empty<string>();
        }
    }

    private static string[] NormalizeRoles(IEnumerable<string> roles)
    {
        return roles
            .Select(role => role?.Trim())
            .Where(role => !string.IsNullOrWhiteSpace(role))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray()!;
    }
}
