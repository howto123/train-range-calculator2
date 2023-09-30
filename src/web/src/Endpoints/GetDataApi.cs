


using System.Threading.Tasks;
using calculator.Export;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace web.Endpoints;

public static class GetDataApi
{
    public static void AddEndpoints(WebApplication app)
    {
        app.MapGet("api/getdata", async (HttpRequest request, HttpResponse response) =>
        {
            Task<byte[]> bytes = JsonStreamExporter.GetCitiesWithStringSteps();
            response.StatusCode = 200;
            response.ContentType = "text/json";
            await response.Body.WriteAsync(await bytes);
        });

        app.MapGet("api/getbasedata", async (HttpRequest request, HttpResponse response) =>
        {
            Task<byte[]> bytes = JsonStreamExporter.GetCitiesWithDirectString();
            response.StatusCode = 200;
            response.ContentType = "text/json";
            await response.Body.WriteAsync(await bytes);
        });
    }

    
}