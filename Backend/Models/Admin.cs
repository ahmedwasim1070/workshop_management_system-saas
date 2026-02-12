using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public enum AuthProvider
{
    Local,
    Google
}

public class Admin
{
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Password { get; set; } = string.Empty;

    [MaxLength(50)]
    public string AuthProvider { get; set; } = string.Empty;

    [MaxLength(255)]
    public string ProviderId { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}