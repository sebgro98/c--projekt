using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.Models;

[Table("roles")] // Mappa klassen till tabellen "roles"
public class Role
{
    [Key] // Ange att detta är primärnyckeln
    [Column("id")] // Mappa egenskapen till kolumnen "id"
    public int Id { get; set; }

    [Column("role_name")] // Mappa egenskapen till kolumnen "role_name"
    [Required] // Gör fältet obligatoriskt
    [MaxLength(50)] // Valfritt: Ange maximal längd för fältet
    public string RoleName { get; set; }

    // Navigation property for the one-to-many relationship
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
