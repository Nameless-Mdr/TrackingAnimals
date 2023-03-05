using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.User;

[Table("users", Schema = "auth")]
public class User
{
    [Column("id")]
    public int Id { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    public string LastName { get; set; } = null!;

    [Column("email")]
    public string Email { get; set; } = null!;

    [Column("password")]
    public string PasswordHash { get; set; } = null!;
    
    
    public virtual ICollection<UserSession>? Sessions { get; set; }
    
    public virtual ICollection<Animal.Animal>? Animals { get; set; }
}