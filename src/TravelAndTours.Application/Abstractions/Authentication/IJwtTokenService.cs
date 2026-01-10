namespace TravelAndTours.Application.Abstractions.Authentication;

public interface IJwtTokenService
{
    Task<string> GenerateTokenAsync(ApplicationUserInfo user, CancellationToken ct);
}
