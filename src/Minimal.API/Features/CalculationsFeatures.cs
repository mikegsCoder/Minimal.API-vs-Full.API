using Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Minimal.API.Features
{
    public static class CalculationsFeatures
    {
        public static void AddCalculations(this IEndpointRouteBuilder app)
        {
            // Examples of using synchronous logic:
            var calcGroup = app.MapGroup("calc");

            calcGroup.MapGet("add", ([FromQuery] int a, [FromQuery] int b, [FromServices] ICalculator calculator) => $"{a} + {b} = {calculator.Add(a, b)}");
            // http://localhost:5000/calc/add?a=2&b=5 -> 2 + 5 = 7
            // http://localhost:5000/calc/add?x=2&y=5 -> Status Code:400 Bad Request

            calcGroup.MapGet("subtract", ([FromQuery] int a, [FromQuery] int b, [FromServices] ICalculator calculator) => $"{a} - {b} = {calculator.Subtract(a, b)}");
            // http://localhost:5000/calc/subtract?a=2&b=5 -> 2 - 5 = -3

            calcGroup.MapGet("multiply", ([FromQuery] int a, [FromQuery] int b, [FromServices] ICalculator calculator) => $"{a} * {b} = {calculator.Multiply(a, b)}");
            // http://localhost:5000/calc/multiply?a=2&b=5 -> 2 * 5 = 10

            calcGroup.MapGet("divide", ([FromQuery] int a, [FromQuery] int b, [FromServices] ICalculator calculator) => $"{a} / {b} = {calculator.Divide(a, b)}");
            // http://localhost:5000/calc/divide?a=10&b=2 -> 10 / 2 = 5

            calcGroup.MapGet("primes", ([FromQuery] int max, [FromServices] ICalculator calculator) => $"Primes to {max} are: {string.Join(", ", calculator.FindAllPrimes(max))}");
            // http://localhost:5000/calc/primes?max=10 -> Primes to 10 are: 2, 3, 5, 7

            calcGroup.MapGet("fibonacci", ([FromQuery] int len, [FromServices] ICalculator calculator) => $"Fibonacci sequence first {len} are: {string.Join(", ", calculator.FibonacciIterative(len))}");
            // http://localhost:5000/calc/fibonacci?len=10 -> Fibonacci sequence first 10 are: 0, 1, 2, 3, 5, 8, 13, 21, 34, 55
        }

        public static void AddCalculationsAsync(this IEndpointRouteBuilder app)
        {
            // Examples of using asynchronous logic:
            var asyncCalcGroup = app.MapGroup("calc-async");

            asyncCalcGroup.MapGet("add", async ([FromQuery] int a, [FromQuery] int b, [FromServices] ICalculator calculator, CancellationToken cancellationToken) =>
            {
                var result = await calculator.AddAsync(a, b, cancellationToken);
                return Results.Ok($"{a} + {b} = {result}");
            });
            // http://localhost:5000/calc-async/add?a=2&b=5 -> 2 + 5 = 7
            // http://localhost:5000/calc-async/add?x=2&y=5 -> Status Code:400 Bad Request

        }
    }
}
