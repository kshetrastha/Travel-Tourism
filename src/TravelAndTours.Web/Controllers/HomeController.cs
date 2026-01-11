using Microsoft.AspNetCore.Mvc;

namespace TravelAndTours.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult Error() => View();
}
