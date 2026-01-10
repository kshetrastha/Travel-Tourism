using MediatR;
using TravelAndTours.Application.Auth.Models;

namespace TravelAndTours.Application.Auth.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;
