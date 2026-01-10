using MediatR;
using TravelAndTours.Application.Auth.Models;

namespace TravelAndTours.Application.Auth.Queries.Me;

public sealed record MeQuery : IRequest<MeResponse>;
