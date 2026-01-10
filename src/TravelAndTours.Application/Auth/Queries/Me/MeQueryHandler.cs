using MediatR;
using TravelAndTours.Application.Abstractions.Authentication;
using TravelAndTours.Application.Auth.Models;
using TravelAndTours.Application.Common.Errors;

namespace TravelAndTours.Application.Auth.Queries.Me;

public sealed class MeQueryHandler : IRequestHandler<MeQuery, MeResponse>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IUserAuthService _auth;

    public MeQueryHandler(ICurrentUserService currentUser, IUserAuthService auth)
    {
        _currentUser = currentUser;
        _auth = auth;
    }

    public async Task<MeResponse> Handle(MeQuery request, CancellationToken ct)
    {
        if (!_currentUser.IsAuthenticated || _currentUser.UserId is null)
            throw new UnauthorizedException("Not authenticated.");

        var info = await _auth.GetUserInfoAsync(_currentUser.UserId.Value, ct);
        if (info is null)
            throw new NotFoundException("User not found.");

        return new MeResponse(info.UserId, info.Email, info.Roles);
    }
}
