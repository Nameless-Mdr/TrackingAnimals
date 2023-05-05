using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.User;

[Table("user_session", Schema = "auth")]
public class UserSession
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("token")]
    public string Token { get; set; } = null!;

    [Column("refresh_token")]
    public string RefreshToken { get; set; } = null!;

    [Column("created_date")]
    public DateTimeOffset CreatedDate { get; set; }

    [Column("expiration_date")]
    public DateTimeOffset ExpirationDate { get; set; }
    
    [NotMapped]
    public bool IsActive => ExpirationDate > DateTimeOffset.UtcNow;

    [Column("ip_address")]
    public string IpAddress { get; set; } = null!;
    
    [Column("is_invalidated")]
    public bool IsInvalidated { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }


    [ForeignKey($"{nameof(UserId)}")]
    public virtual User User { get; set; } = null!;
}