using System.ComponentModel.DataAnnotations;

namespace Authentication.Models.Token;

public class TokenRequestModel
{
    [Required]
    public string Login { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}