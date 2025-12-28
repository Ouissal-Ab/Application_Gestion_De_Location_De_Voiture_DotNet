namespace Data.Entities;

public class Rental
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int VehicleId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Completed, Cancelled
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? QrCodeData { get; set; } // Store QR code data

    // Navigation properties
    public virtual Client Client { get; set; } = null!;
    public virtual Vehicle Vehicle { get; set; } = null!;
}

