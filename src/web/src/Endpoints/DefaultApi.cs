
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace web.Endpoints;

public static class DefaultApi
{
    public static void AddEndpoints(WebApplication app)
    {
        app.MapGet("api", () => "reached");
    }
}