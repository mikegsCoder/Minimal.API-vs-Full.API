namespace Full.API.Controllers;

using Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Core.Models.LogModel;

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

    [HttpPost("log")]
    public async Task<IActionResult> Log([FromBody] LogRequest request, [FromServices] ILogWriter logWriter, CancellationToken cancellationToken)
    {
        var logId = await logWriter.WriteAsync(request.Severity, request.Message, cancellationToken);
        return this.Ok(logId);
    }
    // POST with Postman to http://localhost:5000/info/log
    // Body JSON:
    // {
    //      "Message" : "my message",
    //      "Severity" : 5
    // }
    // Response sattus: 200 OK
    // Response body: "20241116-b89a0b73-9af8-40ec-b2de-a06eec2f7f9a"
    // and .log file is created
}