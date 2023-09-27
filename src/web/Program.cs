
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using web.jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using web.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using web.Endpoints;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using web.Properties;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<RequiredSecrets>();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( o => 
{
    TokenManagerOptions fromSettings = new();
    builder.Configuration.GetSection("TokenManager").Bind(fromSettings);

    try
    {
        o.TokenValidationParameters = new() {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = fromSettings.Issuer,
        ValidAudience = fromSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fromSettings.SecretKey))
        };
    }
    catch
    {
        throw new Exception("You might have forgotten to set a secret hash key 'SecretKey' in the environment variables");
    }
    
});
builder.Services.AddAuthorization();

// for Authorization we can use this to compare claims in the token to values. store values in some settings
// builder.Services.AddAuthorization(options => 
// {
//     options.AddPolicy("myPolicyName", policyBuilder => policyBuilder.RequireClaim("claimName", "claimValue"));
// });
builder.Services.ConfigureOptions<TokenManagerOptionsSetup>();

//this is not working :/
//builder.Services.ConfigureOptions<JwtBaererOptionsSetup>();


builder.Services.AddSingleton<TokenManager>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

GetDataApi.AddEndpoints(app);
UpdateApi.AddEndpoints(app);
LoginApi.AddEndpoints(app);

app.Run();
