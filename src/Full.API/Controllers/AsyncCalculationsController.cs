namespace Full.API.Controllers;

using Core.Contracts;
using Microsoft.AspNetCore.Mvc;

[Route("calc-async")]
public class AsyncCalculationsController(ICalculator calculator) : ControllerBase
{
    private readonly ICalculator _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));

    [HttpGet("add")]
    public async Task<IActionResult> Add([FromQuery] int a, [FromQuery] int b, CancellationToken cancellationToken)
    {
        var result = await this._calculator.AddAsync(a, b, cancellationToken);
        return this.Ok($"{a} + {b} = {result}");
    }
    // http://localhost:5000/calc-async/add?a=2&b=5 -> 2 + 5 = 7
}