using System.ComponentModel.DataAnnotations;

namespace FrontOffice.Web.Models;

public class RentalRequestViewModel
{
    [Required]
    [Display(Name = "Vehicle")]
    public int VehicleId { get; set; }

    [Required]
    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; } = DateTime.Today;

    [Required]
    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

    public string? VehicleInfo { get; set; }
    public decimal? DailyRate { get; set; }
}

