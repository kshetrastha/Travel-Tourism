using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelAndTours.Application.Abstractions.Authentication;
using TravelAndTours.Infrastructure.Identity;

namespace TravelAndTours.Infrastructure.Authentication;

public sealed class UserAuthService : IUserAuthService
{
    private readonly UserManager<ApplicationUser> _users;
    private readonly SignInManager<ApplicationUser> _signIn;
    private readonly RoleManager<ApplicationRole> _roles;

    public UserAuthService(
        UserManager<ApplicationUser> users,
        SignInManager<ApplicationUser> signIn,
        RoleManager<ApplicationRole> roles)
    {
        _users = users;
        _signIn = signIn;
        _roles = roles;
    }

    public async Task<(bool Success, string? Error)> RegisterAsync(string email, string password, CancellationToken ct)
    {
        var existing = await _users.FindByEmailAsync(email);
        if (existing is not null)
            return (false, "Email already registered.");

        var user = new ApplicationUser
        {
            Email = email,
            UserName = email
        };

        var result = await _users.CreateAsync(user, password);
        if (!result.Succeeded)
            return (false, string.Join("; ", result.Errors.Select(e => e.Description)));

        // Ensure USER role exists
        if (!await _roles.RoleExistsAsync("USER"))
            await _roles.CreateAsync(new ApplicationRole { Name = "USER" });

        await _users.AddToRoleAsync(user, "USER");

        return (true, null);
    }

    public async Task<(bool Success, ApplicationUserInfo? User, string? Error)> ValidateCredentialsAsync(string email, string password, CancellationToken ct)
    {
        var user = await _users.FindByEmailAsync(email);
        if (user is null)
            return (false, null, "Invalid email or password.");

        var result = await _signIn.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
        if (!result.Succeeded)
            return (false, null, "Invalid email or password.");

        var roles = await _users.GetRolesAsync(user);

        return (true, new ApplicationUserInfo(user.Id, user.Email ?? email, roles), null);
    }

    public async Task<ApplicationUserInfo?> GetUserInfoAsync(int userId, CancellationToken ct)
    {
        var user = await _users.Users.FirstOrDefaultAsync(u => u.Id == userId, ct);
        if (user is null)
            return null;

        var roles = await _users.GetRolesAsync(user);
        return new ApplicationUserInfo(user.Id, user.Email ?? user.UserName ?? string.Empty, roles);
    }
}
