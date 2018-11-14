using System;

namespace StringEquationv2
{
    /*              OBJECTIVE
     * example equation: -222.120389 (-4 / -2) - 22.1230 + (10.98372 - -5) - 500 ^ 2
     * Format equation properly (change equation from 2(2+2) to 2*(2+2))
     * PE(MD)(AS)
     * parentheses first
     * exponents second
     * multiply or divide left to right
     * add or subtract left to right
     */

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
