﻿using System;

namespace StringEquationv2
{
    class Program
    {
        static void Main(string[] args)
        {
            ComplexEquations solver = new ComplexEquations();
            string value = solver.Calculate("-222.120389 (-4 / -2) - 22.1230 + (10.98372 - -5) - 500 ^ 2");
            Console.WriteLine(value);
            Console.Read();
        }
    }
}
