
using System;
using web.jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using web.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using web.Endpoints;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using web.Properties;
using calculator.Calculator;

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

// inject calculator
var calculatorSettings = builder.Configuration.GetSection("CalculatorSettings")
    .Get<CalculatorSettings>()!;
// string relativeBase = calculatorSettings.RelativeBasePath;
// string relativeOutput = calculatorSettings.RelativeResultPath;
Calculator calculator = new(calculatorSettings);
builder.Services.AddSingleton<Calculator>(calculator);

var app = builder.Build();
Console.WriteLine(app.Services.GetService<Calculator>()?.GetAbsoluteBasePath());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

DefaultApi.AddEndpoints(app);
GetDataApi.AddEndpoints(app);
UpdateApi.AddEndpoints(app);
LoginApi.AddEndpoints(app);

app.Run();
