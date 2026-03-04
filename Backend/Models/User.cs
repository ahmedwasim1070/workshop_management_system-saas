using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

// List of auth providers
public enum AuthProviders
{
    Local,
    Google
}

public class User
{
    public int Id { get; set; }

    public Guid PublicId { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Password { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public AuthProviders Provider { get; set; } = AuthProviders.Local;

    [MaxLength(255)]
    public string? ProviderId { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}