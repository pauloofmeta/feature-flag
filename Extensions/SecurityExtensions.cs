using System.Text;
using Carguero.FeatureFlag.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Carguero.FeatureFlag.Extensions;

public static class SecurityExtensions
{
    public static void AddJwtToken(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenConfig = new TokenConfig();
        configuration.Bind("AuthSettings", tokenConfig);
        services.AddSingleton(tokenConfig);
        
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidIssuer = tokenConfig.IssuerUrl,
                ValidAudience = tokenConfig.IssuerUrl,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromHours(8),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.IssuerSigningKey))
            };
            o.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = ctx =>
                {
                    Console.WriteLine(ctx.Exception.Message);
                    return Task.CompletedTask;
                }
                
            };
        });
    }
}