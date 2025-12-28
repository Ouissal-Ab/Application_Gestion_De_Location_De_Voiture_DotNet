using Microsoft.AspNetCore.Mvc;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrontOffice.Web.Controllers;

public class AdminController : Controller
{
    private readonly CarRentalDbContext _context;
    private readonly ILogger<AdminController> _logger;

    public AdminController(CarRentalDbContext context, ILogger<AdminController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Vérifie si l'utilisateur est admin
    private bool IsAdmin()
    {
        var userType = HttpContext.Session.GetString("UserType");
        return userType == "Admin";
    }

    // Dashboard Admin
    public async Task<IActionResult> Dashboard()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            var stats = new
            {
                TotalVehicles = await _context.Vehicles.CountAsync(),
                AvailableVehicles = await _context.Vehicles.CountAsync(v => v.IsAvailable),
                TotalClients = await _context.Clients.CountAsync(),
                TotalRentals = await _context.Rentals.CountAsync(),
                PendingRentals = await _context.Rentals.CountAsync(r => r.Status == "Pending"),
                ActiveRentals = await _context.Rentals.CountAsync(r => r.Status == "Confirmed"),
                TotalRevenue = await _context.Rentals
                    .Where(r => r.Status == "Confirmed" || r.Status == "Completed")
                    .SumAsync(r => (decimal?)r.TotalAmount) ?? 0
            };

            ViewBag.Stats = stats;
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading admin dashboard");
            ViewBag.ErrorMessage = "Une erreur est survenue lors du chargement du tableau de bord.";
            return View();
        }
    }

    // GESTION DES VÉHICULES
    public async Task<IActionResult> Vehicles()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var vehicles = await _context.Vehicles
            .OrderByDescending(v => v.Id)
            .ToListAsync();

        return View(vehicles);
    }

    [HttpGet]
    public IActionResult CreateVehicle()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateVehicle(Vehicle vehicle)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            return View(vehicle);
        }

        try
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Véhicule créé avec succès !";
            return RedirectToAction("Vehicles");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating vehicle");
            ModelState.AddModelError("", "Une erreur est survenue lors de la création du véhicule.");
            return View(vehicle);
        }
    }

    [HttpGet]
    public IActionResult EditVehicle(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var vehicle = _context.Vehicles.Find(id);
        if (vehicle == null)
        {
            return NotFound();
        }

        return View(vehicle);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditVehicle(int id, Vehicle vehicle)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        if (id != vehicle.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(vehicle);
        }

        try
        {
            _context.Update(vehicle);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Véhicule modifié avec succès !";
            return RedirectToAction("Vehicles");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating vehicle");
            ModelState.AddModelError("", "Une erreur est survenue lors de la modification du véhicule.");
            return View(vehicle);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteVehicle(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            // Vérifier si le véhicule a des locations
            var hasRentals = _context.Rentals.Any(r => r.VehicleId == id);
            if (hasRentals)
            {
                TempData["ErrorMessage"] = "Impossible de supprimer ce véhicule car il a des locations associées.";
                return RedirectToAction("Vehicles");
            }

            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Véhicule supprimé avec succès !";
            return RedirectToAction("Vehicles");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting vehicle");
            TempData["ErrorMessage"] = "Une erreur est survenue lors de la suppression du véhicule.";
            return RedirectToAction("Vehicles");
        }
    }

    // GESTION DES CLIENTS
    public async Task<IActionResult> Clients()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var clients = await _context.Clients
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return View(clients);
    }

    [HttpGet]
    public IActionResult CreateClient()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateClient(Client client)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            return View(client);
        }

        try
        {
            // Vérifier si l'email existe déjà
            if (_context.Clients.Any(c => c.Email == client.Email))
            {
                ModelState.AddModelError("Email", "Cet email est déjà utilisé.");
                return View(client);
            }

            client.CreatedAt = DateTime.Now;
            _context.Clients.Add(client);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Client créé avec succès !";
            return RedirectToAction("Clients");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating client");
            ModelState.AddModelError("", "Une erreur est survenue lors de la création du client.");
            return View(client);
        }
    }

    [HttpGet]
    public IActionResult EditClient(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var client = _context.Clients.Find(id);
        if (client == null)
        {
            return NotFound();
        }

        return View(client);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditClient(int id, Client client)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        if (id != client.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(client);
        }

        try
        {
            // Vérifier si l'email existe déjà pour un autre client
            var existingClient = _context.Clients.FirstOrDefault(c => c.Email == client.Email && c.Id != id);
            if (existingClient != null)
            {
                ModelState.AddModelError("Email", "Cet email est déjà utilisé par un autre client.");
                return View(client);
            }

            // Récupérer le client existant
            var clientInDb = _context.Clients.Find(id);
            if (clientInDb == null)
            {
                return NotFound();
            }

            // Mettre à jour les champs
            clientInDb.FirstName = client.FirstName;
            clientInDb.LastName = client.LastName;
            clientInDb.Email = client.Email;
            clientInDb.Phone = client.Phone;
            
            // Ne mettre à jour le mot de passe que s'il n'est pas vide
            if (!string.IsNullOrWhiteSpace(client.Password))
            {
                clientInDb.Password = client.Password;
            }

            _context.SaveChanges();
            TempData["SuccessMessage"] = "Client modifié avec succès !";
            return RedirectToAction("Clients");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating client");
            ModelState.AddModelError("", "Une erreur est survenue lors de la modification du client.");
            return View(client);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteClient(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            // Vérifier si le client a des locations
            var hasRentals = _context.Rentals.Any(r => r.ClientId == id);
            if (hasRentals)
            {
                TempData["ErrorMessage"] = "Impossible de supprimer ce client car il a des locations associées.";
                return RedirectToAction("Clients");
            }

            _context.Clients.Remove(client);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Client supprimé avec succès !";
            return RedirectToAction("Clients");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting client");
            TempData["ErrorMessage"] = "Une erreur est survenue lors de la suppression du client.";
            return RedirectToAction("Clients");
        }
    }

    // GESTION DES LOCATIONS
    public async Task<IActionResult> Rentals()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var rentals = await _context.Rentals
            .Include(r => r.Client)
            .Include(r => r.Vehicle)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return View(rentals);
    }

    [HttpGet]
    public async Task<IActionResult> RentalDetails(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        var rental = await _context.Rentals
            .Include(r => r.Client)
            .Include(r => r.Vehicle)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (rental == null)
        {
            return NotFound();
        }

        return View(rental);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateRentalStatus(int id, string status)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            var rental = _context.Rentals.Find(id);
            if (rental == null)
            {
                return NotFound();
            }

            rental.Status = status;
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Statut de la location mis à jour avec succès !";
            return RedirectToAction("RentalDetails", new { id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating rental status");
            TempData["ErrorMessage"] = "Une erreur est survenue lors de la mise à jour du statut.";
            return RedirectToAction("RentalDetails", new { id });
        }
    }
}

