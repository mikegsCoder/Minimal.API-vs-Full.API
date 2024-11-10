namespace Full.API.Controllers;

using Core.Contracts;
using Microsoft.AspNetCore.Mvc;

[Route("calc")]
public class CalculationsController(ICalculator calculator) : ControllerBase
{
    private readonly ICalculator _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));

    [HttpGet("add")]
    public IActionResult Add([FromQuery] int a, [FromQuery] int b)
    {
        var result = this._calculator.Add(a, b);
        return this.Ok($"{a} + {b} = {result}");
        // http://localhost:5000/calc/add?a=2&b=5 -> 2 + 5 = 7
    }

    [HttpGet("subtract")]
    public IActionResult Subtract([FromQuery] int a, [FromQuery] int b)
    {
        var result = this._calculator.Subtract(a, b);
        return this.Ok($"{a} - {b} = {result}");
        // http://localhost:5000/calc/subtract?a=2&b=5 -> 2 - 5 = -3
    }

    [HttpGet("multiply")]
    public IActionResult Multiply([FromQuery] int a, [FromQuery] int b)
    {
        var result = this._calculator.Multiply(a, b);
        return this.Ok($"{a} * {b} = {result}");
        // http://localhost:5000/calc/multiply?a=2&b=5 -> 2 * 5 = 10
    }
}