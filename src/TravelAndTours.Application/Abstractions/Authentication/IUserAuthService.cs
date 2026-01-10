namespace TravelAndTours.Application.Abstractions.Authentication;

public interface IUserAuthService
{
    Task<(bool Success, string? Error)> RegisterAsync(string email, string password, CancellationToken ct);
    Task<(bool Success, ApplicationUserInfo? User, string? Error)> ValidateCredentialsAsync(string email, string password, CancellationToken ct);
    Task<ApplicationUserInfo?> GetUserInfoAsync(int userId, CancellationToken ct);
}
