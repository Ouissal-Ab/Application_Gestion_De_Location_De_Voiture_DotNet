using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FrontOffice.Web.Models;

namespace FrontOffice.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var userType = HttpContext.Session.GetString("UserType");
        if (string.IsNullOrEmpty(userType))
        {
            return RedirectToAction("Login", "Account");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
