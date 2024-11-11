﻿namespace Core.Contracts;

public interface ICalculator
{
    int Add(int a, int b);

    int Subtract(int a, int b);

    int Multiply(int a, int b);

    int Divide(int a, int b);

    int[] FindAllPrimes(int max);

    int[] FibonacciIterative(int len);
}