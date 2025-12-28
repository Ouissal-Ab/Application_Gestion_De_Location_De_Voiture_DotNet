using Microsoft.AspNetCore.Mvc;
using Data;
using Data.Entities;
using FrontOffice.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontOffice.Web.Controllers;

public class AccountController : Controller
{
    private readonly CarRentalDbContext _context;
    private readonly ILogger<AccountController> _logger;

    public AccountController(CarRentalDbContext context, ILogger<AccountController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            if (model.UserType == "Admin")
            {
                var admin = _context.Admins.FirstOrDefault(a => 
                    a.Username == model.EmailOrUsername && a.Password == model.Password);
                
                if (admin != null)
                {
                    HttpContext.Session.SetString("UserType", "Admin");
                    HttpContext.Session.SetString("UserId", admin.Id.ToString());
                    HttpContext.Session.SetString("Username", admin.Username);
                    return RedirectToAction("Index", "Home");
                }
            }
            else // Client
            {
                var client = _context.Clients.FirstOrDefault(c => 
                    c.Email == model.EmailOrUsername && c.Password == model.Password);
                
                if (client != null)
                {
                    HttpContext.Session.SetString("UserType", "Client");
                    HttpContext.Session.SetString("UserId", client.Id.ToString());
                    HttpContext.Session.SetString("Username", $"{client.FirstName} {client.LastName}");
                    return RedirectToAction("Index", "Vehicles");
                }
            }

            ModelState.AddModelError("", "Invalid email/username or password.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            ModelState.AddModelError("", "An error occurred during login.");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            // Check if email already exists
            if (_context.Clients.Any(c => c.Email == model.Email))
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View(model);
            }

            var client = new Client
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Password = model.Password, // In production, hash this
                CreatedAt = DateTime.Now
            };

            _context.Clients.Add(client);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration");
            ModelState.AddModelError("", "An error occurred during registration.");
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}

