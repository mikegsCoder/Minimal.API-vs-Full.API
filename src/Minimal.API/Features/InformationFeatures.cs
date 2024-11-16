using Core.Contracts;
using Core.Models.Log;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Minimal.API.Features
{
    public static class InformationFeatures
    {
        public static void AddInformations(this IEndpointRouteBuilder app)
        {
            var infoGroup = app.MapGroup("info");

            infoGroup.MapGet("request-analysis", (HttpRequest request) =>
                $"Request Mehod: {request.Method},\nRequest Path: {request.Path.ToString().Trim()},\nContent Length: {request.ContentLength ?? 0}");
            // http://localhost:5000/info/request-analysis
            // Request Mehod: GET,
            // Request Path: /info/request-analysis,
            // Content Length: 0

            infoGroup.MapPost("log", async ([FromBody] LogRequest request, [FromServices] ILogWriter logWriter, CancellationToken cancellationToken) =>
            {
                var logId = await logWriter.WriteAsync(request.Severity, request.Message, cancellationToken);
                return Results.Ok(logId);
            });
            // POST with Postman to http://localhost:5000/info/log
            // Body JSON:
            // {
            //      "Message" : "my message",
            //      "Severity" : 5
            // }
            // Response sattus: 200 OK
            // Response body: "20241116-f8b0f69b-091c-46a3-bc9d-818176228b74"
            // and .log file is created
        }
    }
}
