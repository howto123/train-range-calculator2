


using System.Threading.Tasks;
using calculator.Calculator;
using calculator.Export;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace web.Endpoints;

public static class GetDataApi
{
    public static void AddEndpoints(WebApplication app)
    {
        app.MapGet("api/getdata", async (HttpRequest request, HttpResponse response, [FromServices] Calculator calculator) =>
        {
            Task<byte[]> bytes = calculator.GetResultFileAsPromiseOfByteStream();
            response.StatusCode = 200;
            response.ContentType = "text/json";
            await response.Body.WriteAsync(await bytes);
        });

        app.MapGet("api/getbasedata", async (HttpRequest request, HttpResponse response, [FromServices] Calculator calculator) =>
        {
            Task<byte[]> bytes = calculator.GetBaseFileAsPromiseOfByteStream();
            response.StatusCode = 200;
            response.ContentType = "text/json";
            await response.Body.WriteAsync(await bytes);
        });
    }
}