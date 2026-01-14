using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginViewModel vm, CancellationToken ct = default);
    Task SignInAsync(AuthResponse res, bool isPersistent, CancellationToken ct = default);
    Task RegisterAsync(RegisterViewModel vm, CancellationToken ct = default);
    Task SignOutAsync(CancellationToken ct = default);
    bool IsSignedIn();
}
