using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Carguero.FeatureFlag.Abstrations;
using Carguero.FeatureFlag.Entities;
using Carguero.FeatureFlag.Models;
using Microsoft.IdentityModel.Tokens;

namespace Carguero.FeatureFlag.Services;

public class UserServices: IUserService
{
    private readonly IRepository<User> _repository;
    private readonly TokenConfig _tokenConfig;

    public UserServices(IRepository<User> repository,
        TokenConfig tokenConfig)
    {
        _repository = repository;
        _tokenConfig = tokenConfig;
    }
    
    public async Task<TokenModel?> AuthenticateAsync(UserLogin login)
    {
        var user = await _repository.GetAsync(x => x.Email == login.Email);
        if (user == default)
            return default;
        
        if (!string.Equals(user.Password, login.Password))
            return default;

        return GenerateToken(user);
    }

    private TokenModel? GenerateToken(User user)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var expireTime = DateTime.UtcNow.AddHours(8);
            var tokenDescriptor = HandleJwtSecurityToken(user, expireTime);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            return new TokenModel
            {
                AccessToken = accessToken, 
                ExpiresIn =  new DateTimeOffset(expireTime).ToUnixTimeMilliseconds()
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return default;
        }
    }

    private SecurityTokenDescriptor HandleJwtSecurityToken(User user, DateTime expires)
    {
        var key = Encoding.UTF8.GetBytes(_tokenConfig.IssuerSigningKey);
        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(HandleCalims(user, expires)),
            Expires = expires,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = _tokenConfig.IssuerUrl,
            Audience = _tokenConfig.IssuerUrl,
            IssuedAt = DateTime.UtcNow,
            NotBefore = new DateTimeOffset(DateTime.Now).DateTime
        };
        // return new JwtSecurityToken(
        //     _tokenConfig.IssuerUrl,
        //     _tokenConfig.IssuerUrl,
        //     HandleCalims(user, expires),
        //     new DateTimeOffset(DateTime.Now).DateTime,
        //     new DateTimeOffset(expires).DateTime,
        //     new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
    }

    private static IEnumerable<Claim> HandleCalims(User user, DateTime expires)
    {
        yield return new Claim("Id", user.Id.ToString());
        yield return new Claim(ClaimTypes.Name, user.Email);
        yield return new Claim(ClaimTypes.Email, user.Email);
        yield return new Claim(ClaimTypes.NameIdentifier, user.Email);
        yield return new Claim("tenant", user.TenantId.ToString());
        yield return new Claim(ClaimTypes.Expiration, expires.ToString("dd/MM/yyyy HH:mm:ss"));
    }
}