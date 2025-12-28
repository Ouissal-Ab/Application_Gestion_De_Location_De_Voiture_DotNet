namespace Data.Entities;

public class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // In production, this should be hashed
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation property
    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}

