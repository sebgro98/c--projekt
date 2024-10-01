using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.Models;

[Table("users")] // Mappa klassen till tabellen "users"
public class User
{
    [Key] // Ange att detta är primärnyckeln
    [Column("id")] // Mappa egenskapen till kolumnen "id"
    public int Id { get; set; }

    [Column("username")] // Mappa egenskapen till kolumnen "username"
    [Required] // Gör fältet obligatoriskt
    [StringLength(50)] // Sätt en maxlängd för strängen
    public string Username { get; set; }

    [Column("password_hash")] // Mappa egenskapen till kolumnen "password_hash"
    [Required]
    public string PasswordHash { get; set; }

    [Column("email")]
    [Required]
    [EmailAddress] // Validera e-postformat
    public string Email { get; set; }

    // Lägg till fler egenskaper för att representera andra kolumner
}
