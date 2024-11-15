using Core.Contracts;
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
        }
    }
}
