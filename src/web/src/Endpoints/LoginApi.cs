using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using web.jwt;

namespace web.Endpoints;
public static class LoginApi
{
    public static void AddEndpoints(WebApplication app)
    {
        app.MapPost("api/login", async (HttpRequest request, HttpResponse response ) => 
        {
            /* Request body needs to have this shape:
                {
                    "password": "mypassword"
                }
            */
            
            var bodyString = await new StreamReader(request.Body).ReadToEndAsync();

            // parse body and extract password
            Dictionary<string, string>? pairs;
            try
            {
                pairs = JsonSerializer.Deserialize<Dictionary<string, string>>(bodyString);
            }
            catch
            {
                return Results.Problem("The request-body was not what we expected...", null, 401);
            }
            string? password = pairs?.GetValueOrDefault("password");
            var secret = app.Configuration["loginpassword"];

            if(password?.Equals(secret) ?? false)
            {
                var claims = new Dictionary<string, string>
                {
                    { "any who knows the password", "has access to the api" },
                    { "no special cases", "with special rights" }
                };
                
                // injected because that way it has the required config
                var tokenManager = app.Services.GetRequiredService<TokenManager>();
                var token = tokenManager.Create(claims);

                response.Cookies.Append("token", token);

                Console.WriteLine("Login succeeded at : " + DateTime.UtcNow);
                
                return Results.Ok("Successful!");
            }

            // To Do Logging?
            Console.WriteLine("Login failed at : " + DateTime.UtcNow);

            return Results.Problem($"Sorry, login failed", null, 401);
        });
    }
}