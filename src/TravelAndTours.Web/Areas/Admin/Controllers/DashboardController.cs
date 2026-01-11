using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAndTours.Web.Filters;

namespace TravelAndTours.Web.Areas.Admin.Controllers;

[Area("Admin")]
[RequireRole("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
