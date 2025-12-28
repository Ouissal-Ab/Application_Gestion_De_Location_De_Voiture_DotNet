using Data;
using Data.Entities;

namespace FrontOffice.Web.Services;

public class DataInitializer
{
    private readonly CarRentalDbContext _context;

    public DataInitializer(CarRentalDbContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        try
        {
            // Créer l'admin Hatim Bendani - toujours s'assurer qu'il existe
            var existingAdmin = _context.Admins.FirstOrDefault(a => a.Username == "hatimlbg12");
            if (existingAdmin == null)
            {
                // Vérifier s'il y a un admin par défaut avec username "admin"
                var defaultAdmin = _context.Admins.FirstOrDefault(a => a.Username == "admin");
                if (defaultAdmin != null)
                {
                    // Mettre à jour l'admin par défaut
                    defaultAdmin.Username = "hatimlbg12";
                    defaultAdmin.Password = "hatimlbg12";
                    defaultAdmin.Email = "hatim.bendani@carrental.com";
                }
                else
                {
                    // Créer un nouvel admin
                    _context.Admins.Add(new Admin
                    {
                        Username = "hatimlbg12",
                        Password = "hatimlbg12",
                        Email = "hatim.bendani@carrental.com"
                    });
                }
            }

            // Créer les clients de test
            var clients = new[]
            {
                new { FirstName = "Oussal", LastName = "Aboussaid", Email = "oussal.aboussaid@email.com", Phone = "0612345678", Password = "oussal123" },
                new { FirstName = "Achraf", LastName = "Zahwani", Email = "achraf.zahwani@email.com", Phone = "0612345679", Password = "achraf123" },
                new { FirstName = "Khalid", LastName = "Mohammed Badii", Email = "khalid.badii@email.com", Phone = "0612345680", Password = "khalid123" },
                new { FirstName = "Aymen", LastName = "Adil Bouhou", Email = "aymen.bouhou@email.com", Phone = "0612345681", Password = "aymen123" }
            };

            foreach (var clientData in clients)
            {
                if (!_context.Clients.Any(c => c.Email == clientData.Email))
                {
                    _context.Clients.Add(new Client
                    {
                        FirstName = clientData.FirstName,
                        LastName = clientData.LastName,
                        Email = clientData.Email,
                        Phone = clientData.Phone,
                        Password = clientData.Password,
                        CreatedAt = DateTime.Now
                    });
                }
            }

            // Créer des véhicules de test - s'assurer qu'il y a toujours des véhicules disponibles
            var vehicleCount = _context.Vehicles.Count();
            if (vehicleCount == 0)
            {
                var vehicles = new[]
                {
                    new Vehicle { Brand = "Mercedes-Benz", Model = "Classe C", Year = 2023, Color = "Noir", LicensePlate = "ABC-123", DailyRate = 150.00m, IsAvailable = true, Description = "Berline de luxe avec toutes les options" },
                    new Vehicle { Brand = "BMW", Model = "Série 3", Year = 2023, Color = "Blanc", LicensePlate = "DEF-456", DailyRate = 140.00m, IsAvailable = true, Description = "Conduite sportive et confortable" },
                    new Vehicle { Brand = "Audi", Model = "A4", Year = 2022, Color = "Gris", LicensePlate = "GHI-789", DailyRate = 135.00m, IsAvailable = true, Description = "Élégance et performance" },
                    new Vehicle { Brand = "Volkswagen", Model = "Golf 8", Year = 2023, Color = "Bleu", LicensePlate = "JKL-012", DailyRate = 80.00m, IsAvailable = true, Description = "Compacte et économique" },
                    new Vehicle { Brand = "Peugeot", Model = "308", Year = 2023, Color = "Rouge", LicensePlate = "MNO-345", DailyRate = 75.00m, IsAvailable = true, Description = "Idéale pour la ville" },
                    new Vehicle { Brand = "Renault", Model = "Clio 5", Year = 2022, Color = "Jaune", LicensePlate = "PQR-678", DailyRate = 65.00m, IsAvailable = true, Description = "Compacte et moderne" },
                    new Vehicle { Brand = "Toyota", Model = "Corolla", Year = 2023, Color = "Argent", LicensePlate = "STU-901", DailyRate = 90.00m, IsAvailable = true, Description = "Fiabilité et économie" },
                    new Vehicle { Brand = "Ford", Model = "Focus", Year = 2022, Color = "Vert", LicensePlate = "VWX-234", DailyRate = 85.00m, IsAvailable = true, Description = "Polyvalente et spacieuse" }
                };

                _context.Vehicles.AddRange(vehicles);
            }

            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Log l'erreur mais ne pas faire échouer l'application
            Console.WriteLine($"Error in DataInitializer: {ex.Message}");
            throw;
        }
    }
}

