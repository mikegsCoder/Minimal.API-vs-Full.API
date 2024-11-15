namespace Full.API.Controllers;

using Core.Contracts;
using Microsoft.AspNetCore.Mvc;

[Route("info")]
public class InfoController : ControllerBase
{
    [HttpGet("request-analysis")]
    public IActionResult RequestAnalysis()
        => this.Ok($"Request Mehod: {this.Request.Method},\nRequest Path: {this.Request.Path.ToString().Trim()},\nContent Length: {this.Request.ContentLength ?? 0}");
    // http://localhost:5000/info/request-analysis
    // Request Mehod: GET,
    // Request Path: /info/request-analysis,
    // Content Length: 0
}