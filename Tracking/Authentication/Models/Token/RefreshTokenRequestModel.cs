using System.ComponentModel.DataAnnotations;

namespace Authentication.Models.Token;

public class RefreshTokenRequestModel
{
    [Required]
    public string ExpiredToken { get; set; } = null!;
    
    [Required]
    public string RefreshToken { get; set; } = null!;
}