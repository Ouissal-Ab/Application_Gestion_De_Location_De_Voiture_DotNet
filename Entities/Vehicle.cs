namespace Data.Entities;

public class Vehicle
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Color { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
    public decimal DailyRate { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string? Description { get; set; }

    // Navigation property
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}

