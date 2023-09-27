



using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using calculator.Export;

namespace web.Endpoints;
public static class UpdateApi
{
    public static void AddEndpoints(WebApplication app)
    {
        app.MapPost("/api/update", (HttpRequest request) =>
        {

            return Results.Ok("Another answer from the server.");
        }).RequireAuthorization();
        
        app.MapGet("/api/update", async () =>
        {
            var mimeType = "text/json";
            var path = FileHandler.GetJsonPath();
            var bytes = await File.ReadAllBytesAsync(path);

            return Results.File(bytes, mimeType, "dataToBeAddedTo.json");
        });
    }
}