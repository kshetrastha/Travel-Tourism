using System.ComponentModel.DataAnnotations;
namespace TravelAndTours.Web.Models;

public sealed class LoginViewModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    public bool RememberMe { get; set; }
}

public sealed class RegisterViewModel
{
    [Required]
    public string FullName { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = "";
}

// Matches typical API payloads (adjust if your API differs)
public sealed class AuthResponse
{
    public string Token { get; set; } = "";
    public DateTime? ExpiresAt { get; set; }

    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string[] Roles { get; set; } = Array.Empty<string>();
}
