



using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using calculator.Export;

namespace web.Endpoints;
public static class DefaultApi
{
    public static void AddEndpoints(WebApplication app)
    {
        app.MapPost("/api", (HttpRequest request) =>
        {

            return Results.Ok("Another answer from the server.");
        }).RequireAuthorization();
        
        app.MapGet("/api", async () =>
        {
            Console.WriteLine("request received!");
            var mimeType = "text/json";

            //path from project root, NOT relative to this file
            
            var path = FileHandler.GetJsonPath();
            var bytes = await File.ReadAllBytesAsync(path);

            return Results.File(bytes, mimeType, "dataToBeAddedTo.json");
        });
    }
}