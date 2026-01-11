using TravelAndTours.Web.Models;

namespace TravelAndTours.Web.Services;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginViewModel vm, CancellationToken ct = default);
    Task RegisterAsync(RegisterViewModel vm, CancellationToken ct = default);
    void SignOut();
    bool IsSignedIn();
}
