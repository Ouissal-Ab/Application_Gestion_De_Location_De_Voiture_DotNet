using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data;

public class CarRentalDbContext : DbContext
{
    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
        : base(options)
    {
    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Client)
            .WithMany(c => c.Rentals)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Vehicle)
            .WithMany(v => v.Rentals)
            .HasForeignKey(r => r.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed data pour l'admin initial
        // Note: L'admin sera aussi créé/mis à jour par DataInitializer pour garantir son existence
        modelBuilder.Entity<Admin>().HasData(
            new Admin
            {
                Id = 1,
                Username = "hatimlbg12",
                Password = "hatimlbg12",
                Email = "hatim.bendani@carrental.com"
            }
        );
    }
}
