namespace Core.Services;

using Core.Contracts;

public class Calculator : ICalculator
{
    public int Add(int a, int b) => a + b;

    public int Subtract(int a, int b) => a - b;

    public int Multiply(int a, int b) => a * b;

    public int Divide(int a, int b) => a / b;

    public int[] FindAllPrimes(int max)
    {
        var primes = new List<int>();

        for (var i = 2; i <= max; i++)
        {
            var isPrime = true;
            foreach (var p in primes)
            {
                if (i % p == 0) { isPrime = false; break; }
                if (p * p > i) break;
            }

            if (isPrime) primes.Add(i);
        }

        return primes.ToArray();
    }
}