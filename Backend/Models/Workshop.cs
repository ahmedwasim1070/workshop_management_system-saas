using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Workshop
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;

    [Required]
    [MaxLength(20)]
    public string SubscriptionPlan { get; set; } = "Free";

    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<Role> CustomRoles { get; set; } = new List<Role>();
}