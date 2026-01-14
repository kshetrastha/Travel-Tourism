using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Web.Models;
using TravelAndTours.Web.Services;

namespace TravelAndTours.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpGet]
    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var res = await _auth.LoginAsync(vm, ct);
        if (res is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(vm);
        }

        await _auth.SignInAsync(res, vm.RememberMe, ct);

        var isAdmin = res.Roles.Any(role => role.Equals("admin", StringComparison.OrdinalIgnoreCase));
        if (isAdmin)
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(vm);

        await _auth.RegisterAsync(vm, ct);
        // Redirect to login so the user can sign in and receive token
        return RedirectToAction(nameof(Login));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout(CancellationToken ct)
    {
        await _auth.SignOutAsync(ct);
        return RedirectToAction("Index", "Home");
    }
}
