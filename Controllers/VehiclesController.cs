using Microsoft.AspNetCore.Mvc;
using Data;
using Microsoft.EntityFrameworkCore;

namespace FrontOffice.Web.Controllers;

public class VehiclesController : Controller
{
    private readonly CarRentalDbContext _context;
    private readonly ILogger<VehiclesController> _logger;

    public VehiclesController(CarRentalDbContext context, ILogger<VehiclesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            // Check if user is logged in as Client
            var userType = HttpContext.Session.GetString("UserType");
            if (userType != "Client")
            {
                return RedirectToAction("Login", "Account");
            }

            var vehicles = await _context.Vehicles
                .Where(v => v.IsAvailable)
                .ToListAsync();

            return View(vehicles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading vehicles");
            ViewBag.ErrorMessage = "Une erreur est survenue lors du chargement des véhicules. Veuillez réessayer plus tard.";
            return View(new List<Data.Entities.Vehicle>());
        }
    }
}

