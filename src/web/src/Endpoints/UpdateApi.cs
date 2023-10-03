using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using calculator.JsonInterface;
using calculator.Calculator;
using System;
using System.Threading.Tasks;

namespace web.Endpoints;
public static class UpdateApi
{
    public static void AddEndpoints(WebApplication app)
    {
        app.MapPost("/api/update", ([FromBody] List<CityJson> incomming) =>
        {
            // inject this
            string relativeInput = app.Configuration["FileManager:basePath"]!;
            string relativeOutput = app.Configuration["FileManager:resultPath"]!;
            Calculator calculator = new(relativeInput, relativeOutput);

            // give the updated data to calculate from
            calculator.UpdateFromList(incomming);

            // do the calculation
            int stepNumbers = Int32.Parse(app.Configuration["Calculator:stepNumbers"]!);
            calculator.Execute(stepNumbers);

            return Results.Ok("Another answer from the server.");
        }).RequireAuthorization();
        

        app.MapGet("/api/update", async () =>
        {
            // inject this
            string relativeInput = app.Configuration["FileManager:basePath"]!;
            string relativeOutput = app.Configuration["FileManager:resultPath"]!;
            Calculator calculator = new(relativeInput, relativeOutput);

            var bytes = await calculator.GetBaseFileAsPromiseOfByteStream();
            var mimeType = "text/json";
            

            return Results.File(bytes, mimeType, "dataToBeAddedTo.json");
        });
    }
}