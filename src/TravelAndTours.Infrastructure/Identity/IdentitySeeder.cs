using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelAndTours.Infrastructure.Persistence;

namespace TravelAndTours.Infrastructure.Identity;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services, CancellationToken ct)
    {
        using var scope = services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync(ct);

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var roles = new[] { "ADMIN", "USER" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = role });
            }
        }

        const string adminEmail = "admin@local.test";
        const string adminPassword = "Admin@12345";

        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var create = await userManager.CreateAsync(admin, adminPassword);
            if (!create.Succeeded)
            {
                var msg = string.Join("; ", create.Errors.Select(e => e.Description));
                throw new InvalidOperationException("Admin user creation failed: " + msg);
            }
        }

        if (!await userManager.IsInRoleAsync(admin, "ADMIN"))
            await userManager.AddToRoleAsync(admin, "ADMIN");
    }
}
