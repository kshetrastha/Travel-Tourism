using FluentValidation;
using MediatR;
using TravelAndTours.Application.Abstractions.Authentication;

namespace TravelAndTours.Application.Auth.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IUserAuthService _auth;

    public RegisterCommandHandler(IUserAuthService auth)
    {
        _auth = auth;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken ct)
    {
        var (success, error) = await _auth.RegisterAsync(request.Email, request.Password, ct);
        if (!success)
            throw new ValidationException(error ?? "Registration failed.");

        return true;
    }
}
