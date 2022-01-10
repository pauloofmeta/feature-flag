namespace Carguero.FeatureFlag.Models;

public class TokenModel
{
    public string AccessToken { get; set; }
    public long ExpiresIn { get; set; }
}