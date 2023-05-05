namespace Authentication.Models.Token;

public class TokenModel
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
    
    public bool IsSuccess { get; set; }

    public string Reason { get; set; } = null!;
}