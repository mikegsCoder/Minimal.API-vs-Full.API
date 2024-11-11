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

    [HttpGet("divide")]
    public IActionResult Divide([FromQuery] int a, [FromQuery] int b)
    {
        var result = this._calculator.Divide(a, b);
        return this.Ok($"{a} / {b} = {result}");
        // http://localhost:5000/calc/divide?a=10&b=2 -> 10 / 2 = 5
    }

    [HttpGet("primes")]
    public IActionResult Primes([FromQuery] int max)
    {
        var result = this._calculator.FindAllPrimes(max);
        return this.Ok($"Primes to {max} are: {string.Join(", ", result)}");
        // http://localhost:5000/calc/primes?max=10 -> Primes to 10 are: 2, 3, 5, 7
    }

    [HttpGet("fibonacci")]
    public IActionResult Fibonacci([FromQuery] int len)
    {
        var result = this._calculator.FibonacciIterative(len);
        return this.Ok($"Fibonacci sequence first {len} are: {string.Join(", ", result)}");
        // http://localhost:5000/calc/fibonacci?len=10 -> Fibonacci sequence first 10 are: 0, 1, 2, 3, 5, 8, 13, 21, 34, 55
    }
}