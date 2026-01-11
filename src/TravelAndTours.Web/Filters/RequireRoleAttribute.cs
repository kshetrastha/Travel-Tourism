using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TravelAndTours.Web.Services;

namespace TravelAndTours.Web.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class RequireRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;

    public RequireRoleAttribute(string role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var session = context.HttpContext.Session;
        var token = session.GetString(SessionKeys.AccessToken);
        if (string.IsNullOrWhiteSpace(token))
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
            return;
        }

        var roles = (session.GetString(SessionKeys.UserRoles) ?? "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (roles.Length == 0 || !roles.Contains(_role, StringComparer.OrdinalIgnoreCase))
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}
