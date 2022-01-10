using Carguero.FeatureFlag.Models;

namespace Carguero.FeatureFlag.Abstrations;

public interface IUserService
{
    Task<TokenModel?> AuthenticateAsync(UserLogin login);
}