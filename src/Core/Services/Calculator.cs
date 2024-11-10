namespace Core.Services;

using Core.Contracts;

public class Calculator : ICalculator
{
    public int Add(int a, int b) => a + b;

    public int Subtract(int a, int b) => a - b;
}