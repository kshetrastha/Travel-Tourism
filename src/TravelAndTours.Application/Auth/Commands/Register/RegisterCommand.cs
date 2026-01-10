using MediatR;
using TravelAndTours.Application.Auth.Models;

namespace TravelAndTours.Application.Auth.Commands.Register;

public sealed record RegisterCommand(string Email, string Password) : IRequest<bool>;
