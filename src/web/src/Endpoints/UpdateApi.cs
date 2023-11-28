using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using calculator.Calculator;
using System;
using calculator.CityTypes;

namespace web.Endpoints;
public static class UpdateApi
{
    public static void AddEndpoints(WebApplication app)
    {
        app.MapPost
        (
            "/api/update",
            (
                [FromBody] List<CityNameList> incomming,
                [FromServices] Calculator calculator
            )
                =>
            {
                // give the updated data to calculate from
                calculator.UpdateFromList(incomming);

                // do the calculation
                int stepNumbers = Int32.Parse(app.Configuration["Calculator:stepNumbers"]!);
                calculator.Execute(stepNumbers);

                return Results.Ok("Upload successful");
            }
        ).RequireAuthorization();
        

        app.MapGet("/api/update", async ([FromServices] Calculator calculator) =>
        {
            var bytes = await calculator.GetBaseFileAsPromiseOfByteStream();
            var mimeType = "text/json";

            return Results.File(bytes, mimeType, "dataToBeAddedTo.json");
        });
    }
}