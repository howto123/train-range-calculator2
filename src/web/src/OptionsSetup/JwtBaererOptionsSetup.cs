


using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using web.jwt;

namespace web.OptionsSetup;

public class JwtBaererOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
    private readonly TokenManagerOptions _managerOptions;

    public JwtBaererOptionsSetup(IOptions<TokenManagerOptions> managerOptions)
    {
        _managerOptions = managerOptions.Value;
    }

    public void Configure(JwtBearerOptions options)
    {
        Console.WriteLine($"Manager Options Key is: {_managerOptions.SecretKey}");
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _managerOptions.Issuer,
            ValidAudience = _managerOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_managerOptions.SecretKey))
        };
    }
}

