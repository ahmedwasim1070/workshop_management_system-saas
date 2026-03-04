using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Permission
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

}