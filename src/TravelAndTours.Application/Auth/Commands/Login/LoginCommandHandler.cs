using MediatR;
using TravelAndTours.Application.Abstractions.Authentication;
using TravelAndTours.Application.Auth.Models;
using TravelAndTours.Application.Common.Errors;

namespace TravelAndTours.Application.Auth.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IUserAuthService _auth;
    private readonly IJwtTokenService _jwt;

    public LoginCommandHandler(IUserAuthService auth, IJwtTokenService jwt)
    {
        _auth = auth;
        _jwt = jwt;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken ct)
    {
        var (success, user, error) = await _auth.ValidateCredentialsAsync(request.Email, request.Password, ct);
        if (!success || user is null)
            throw new UnauthorizedException(error ?? "Invalid credentials.");

        var token = await _jwt.GenerateTokenAsync(user, ct);

        // Expiry is read from JwtOptions in Infrastructure; set client-friendly expiry here as "now + 60" by default.
        // Infrastructure token uses the same expiry minutes, so this matches.
        var expiresAt = DateTime.UtcNow.AddMinutes(60);

        return new AuthResponse(token, expiresAt, user.UserId, user.Email, user.Roles);
    }
}
