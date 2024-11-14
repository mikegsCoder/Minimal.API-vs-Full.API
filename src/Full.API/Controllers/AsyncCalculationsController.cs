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

    [HttpGet("subtract")]
    public async Task<IActionResult> Subtract([FromQuery] int a, [FromQuery] int b, CancellationToken cancellationToken)
    {
        var result = await this._calculator.SubtractAsync(a, b, cancellationToken);
        return this.Ok($"{a} - {b} = {result}");
    }
    // http://localhost:5000/calc-async/subtract?a=2&b=5 -> 2 - 5 = -3

    [HttpGet("multiply")]
    public async Task<IActionResult> Multiply([FromQuery] int a, [FromQuery] int b, CancellationToken cancellationToken)
    {
        var result = await this._calculator.MultiplyAsync(a, b, cancellationToken);
        return this.Ok($"{a} * {b} = {result}");
    }
    // http://localhost:5000/calc-async/multiply?a=2&b=5 -> 2 * 5 = 10

    [HttpGet("divide")]
    public async Task<IActionResult> Divide([FromQuery] int a, [FromQuery] int b, CancellationToken cancellationToken)
    {
        var result = await this._calculator.DivideAsync(a, b, cancellationToken);
        return this.Ok($"{a} / {b} = {result}");
    }
    // http://localhost:5000/calc-async/divide?a=10&b=2 -> 10 / 2 = 5

    [HttpGet("primes")]
    public async Task<IActionResult> Primes([FromQuery] int max, CancellationToken cancellationToken)
    {
        var result = await this._calculator.FindAllPrimesAsync(max, cancellationToken);
        return this.Ok($"Primes to {max} are: {string.Join(", ", result)}");
    }
    // http://localhost:5000/calc-async/primes?max=10 -> Primes to 10 are: 2, 3, 5, 7
}