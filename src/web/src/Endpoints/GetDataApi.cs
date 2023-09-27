


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
            Task<byte[]> bytes = JsonStreamExporter.GetCityWithStringSteps();
            response.StatusCode = 200;
            await response.Body.WriteAsync(await bytes);
        });
    }
}