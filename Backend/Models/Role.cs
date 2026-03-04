using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Role
{
    public int id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = String.Empty;

    public int WorkshopId { get; set; }
    public Workshop Workshop { get; set; } = null!;

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<User> Users { get; set; } = new List<User>();
}