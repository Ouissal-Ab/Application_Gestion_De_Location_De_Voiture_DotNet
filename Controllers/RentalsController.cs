using Microsoft.AspNetCore.Mvc;
using Data;
using Data.Entities;
using FrontOffice.Web.Models;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FrontOffice.Web.Controllers;

public class RentalsController : Controller
{
    private readonly CarRentalDbContext _context;
    private readonly ILogger<RentalsController> _logger;

    public RentalsController(CarRentalDbContext context, ILogger<RentalsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
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

            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int clientId))
            {
                return RedirectToAction("Login", "Account");
            }

            var rentals = await _context.Rentals
                .Include(r => r.Vehicle)
                .Where(r => r.ClientId == clientId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return View(rentals);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading rentals");
            ViewBag.ErrorMessage = "Une erreur est survenue lors du chargement de vos réservations.";
            return View(new List<Rental>());
        }
    }

    [HttpGet]
    public IActionResult RequestRental(int vehicleId)
    {
        // Check if user is logged in as Client
        var userType = HttpContext.Session.GetString("UserType");
        if (userType != "Client")
        {
            return RedirectToAction("Login", "Account");
        }

        var vehicle = _context.Vehicles.Find(vehicleId);
        if (vehicle == null || !vehicle.IsAvailable)
        {
            return NotFound();
        }

        var model = new RentalRequestViewModel
        {
            VehicleId = vehicleId,
            VehicleInfo = $"{vehicle.Brand} {vehicle.Model} ({vehicle.Year})",
            DailyRate = vehicle.DailyRate,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1)
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RequestRental(RentalRequestViewModel model)
    {
        // Check if user is logged in as Client
        var userType = HttpContext.Session.GetString("UserType");
        if (userType != "Client")
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            var vehicle = _context.Vehicles.Find(model.VehicleId);
            if (vehicle != null)
            {
                model.VehicleInfo = $"{vehicle.Brand} {vehicle.Model} ({vehicle.Year})";
                model.DailyRate = vehicle.DailyRate;
            }
            return View(model);
        }

        if (model.EndDate <= model.StartDate)
        {
            ModelState.AddModelError("EndDate", "End date must be after start date.");
            var vehicle = _context.Vehicles.Find(model.VehicleId);
            if (vehicle != null)
            {
                model.VehicleInfo = $"{vehicle.Brand} {vehicle.Model} ({vehicle.Year})";
                model.DailyRate = vehicle.DailyRate;
            }
            return View(model);
        }

        try
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int clientId))
            {
                return RedirectToAction("Login", "Account");
            }

            var vehicle = _context.Vehicles.Find(model.VehicleId);
            if (vehicle == null || !vehicle.IsAvailable)
            {
                ModelState.AddModelError("", "Vehicle is not available.");
                return View(model);
            }

            var days = (model.EndDate - model.StartDate).Days;
            var totalAmount = vehicle.DailyRate * days;

            var rental = new Rental
            {
                ClientId = clientId,
                VehicleId = model.VehicleId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                TotalAmount = totalAmount,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            _context.Rentals.Add(rental);
            _context.SaveChanges();

            // Generate QR Code
            var qrData = $"Rental ID: {rental.Id}\nClient: {clientId}\nVehicle: {vehicle.Brand} {vehicle.Model}\nStart: {rental.StartDate:yyyy-MM-dd}\nEnd: {rental.EndDate:yyyy-MM-dd}";
            rental.QrCodeData = qrData;
            _context.SaveChanges();

            // Send confirmation email (fake)
            SendConfirmationEmail(rental);

            TempData["SuccessMessage"] = $"Rental request submitted successfully! Rental ID: {rental.Id}";
            TempData["RentalId"] = rental.Id;
            TempData["ShowPdfLink"] = true;
            return RedirectToAction("Index", "Vehicles");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating rental");
            ModelState.AddModelError("", "An error occurred while processing your request.");
            var vehicle = _context.Vehicles.Find(model.VehicleId);
            if (vehicle != null)
            {
                model.VehicleInfo = $"{vehicle.Brand} {vehicle.Model} ({vehicle.Year})";
                model.DailyRate = vehicle.DailyRate;
            }
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult QrCode(int id)
    {
        var rental = _context.Rentals
            .Include(r => r.Client)
            .Include(r => r.Vehicle)
            .FirstOrDefault(r => r.Id == id);

        if (rental == null)
        {
            return NotFound();
        }

        var qrData = rental.QrCodeData ?? $"Rental ID: {rental.Id}\nClient: {rental.Client.FirstName} {rental.Client.LastName}\nVehicle: {rental.Vehicle.Brand} {rental.Vehicle.Model}\nStart: {rental.StartDate:yyyy-MM-dd}\nEnd: {rental.EndDate:yyyy-MM-dd}";

        using QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
        using QRCode qrCode = new QRCode(qrCodeData);
        Bitmap qrBitmap = qrCode.GetGraphic(20);

        using var ms = new MemoryStream();
        qrBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        return File(ms.ToArray(), "image/png");
    }

    [HttpGet]
    public IActionResult DownloadPdf(int id)
    {
        var rental = _context.Rentals
            .Include(r => r.Client)
            .Include(r => r.Vehicle)
            .FirstOrDefault(r => r.Id == id);

        if (rental == null)
        {
            return NotFound();
        }

        // Check if user is logged in as Client and owns this rental
        var userType = HttpContext.Session.GetString("UserType");
        var userId = HttpContext.Session.GetString("UserId");
        if (userType != "Client" || userId != rental.ClientId.ToString())
        {
            return Unauthorized();
        }

        QuestPDF.Settings.License = LicenseType.Community;
        
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .AlignCenter()
                    .Text("CONFIRMATION DE RÉSERVATION")
                    .FontSize(20f)
                    .Bold()
                    .FontColor(Colors.Blue.Darken2);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Spacing(1, Unit.Centimetre);

                        column.Item().Text($"Numéro de réservation: #{rental.Id}").Bold().FontSize(14f);
                        column.Item().Text($"Date de création: {rental.CreatedAt:dd/MM/yyyy HH:mm}").FontSize(12f);
                        
                        column.Item().PaddingTop(0.5f, Unit.Centimetre).LineHorizontal(1f).LineColor(Colors.Grey.Medium);
                        
                        column.Item().Text("INFORMATIONS CLIENT").Bold().FontSize(14f).FontColor(Colors.Blue.Darken1);
                        column.Item().Text($"Nom: {rental.Client.FirstName} {rental.Client.LastName}");
                        column.Item().Text($"Email: {rental.Client.Email}");
                        column.Item().Text($"Téléphone: {rental.Client.Phone}");
                        
                        column.Item().PaddingTop(0.5f, Unit.Centimetre).LineHorizontal(1f).LineColor(Colors.Grey.Medium);
                        
                        column.Item().Text("INFORMATIONS VÉHICULE").Bold().FontSize(14f).FontColor(Colors.Blue.Darken1);
                        column.Item().Text($"Véhicule: {rental.Vehicle.Brand} {rental.Vehicle.Model}");
                        column.Item().Text($"Année: {rental.Vehicle.Year}");
                        column.Item().Text($"Couleur: {rental.Vehicle.Color}");
                        column.Item().Text($"Plaque d'immatriculation: {rental.Vehicle.LicensePlate}");
                        
                        column.Item().PaddingTop(0.5f, Unit.Centimetre).LineHorizontal(1f).LineColor(Colors.Grey.Medium);
                        
                        column.Item().Text("DÉTAILS DE LA LOCATION").Bold().FontSize(14f).FontColor(Colors.Blue.Darken1);
                        column.Item().Text($"Date de début: {rental.StartDate:dd/MM/yyyy}");
                        column.Item().Text($"Date de fin: {rental.EndDate:dd/MM/yyyy}");
                        var days = (rental.EndDate - rental.StartDate).Days;
                        column.Item().Text($"Durée: {days} jour(s)");
                        column.Item().Text($"Tarif journalier: {rental.Vehicle.DailyRate:C}");
                        column.Item().PaddingTop(0.3f, Unit.Centimetre);
                        column.Item().Text($"Montant total: {rental.TotalAmount:C}").Bold().FontSize(16f).FontColor(Colors.Green.Darken2);
                        
                        column.Item().PaddingTop(0.5f, Unit.Centimetre).LineHorizontal(1f).LineColor(Colors.Grey.Medium);
                        
                        column.Item().Text($"Statut: {rental.Status}").Bold().FontSize(12f);
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Car Rental System - ").FontSize(10f).FontColor(Colors.Grey.Medium);
                        text.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).FontSize(10f).FontColor(Colors.Grey.Medium);
                    });
            });
        });
        
        var pdfBytes = document.GeneratePdf();

        return File(pdfBytes, "application/pdf", $"Reservation_{rental.Id}.pdf");
    }

    private void SendConfirmationEmail(Rental rental)
    {
        // Fake email sending - just log it
        var client = _context.Clients.Find(rental.ClientId);
        var emailBody = $@"
Bonjour {client?.FirstName} {client?.LastName},

Votre demande de location a été confirmée!

Détails de la réservation:
- Numéro: #{rental.Id}
- Véhicule: {rental.Vehicle.Brand} {rental.Vehicle.Model}
- Période: {rental.StartDate:dd/MM/yyyy} au {rental.EndDate:dd/MM/yyyy}
- Montant total: {rental.TotalAmount:C}

Vous pouvez télécharger votre confirmation PDF et voir votre QR Code sur le site.

Cordialement,
L'équipe Car Rental System
";
        _logger.LogInformation($"📧 Email envoyé à {client?.Email}:\n{emailBody}");
    }
}

