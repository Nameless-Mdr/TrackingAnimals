using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.User;

public class UserSession
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("refresh_token")]
    public Guid RefreshToken { get; set; }

    [Column("created")]
    public DateTimeOffset Created { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;


    public virtual User User { get; set; } = null!;
}