using System.ComponentModel.DataAnnotations;

namespace FrontOffice.Web.Models;

public class LoginViewModel
{
    [Required]
    [Display(Name = "User Type")]
    public string UserType { get; set; } = "Client"; // Admin or Client

    [Required]
    [Display(Name = "Email/Username")]
    public string EmailOrUsername { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}

