using System;

namespace PrimeNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            long maxNumber = 1000000;
            string filePath = @"numbers.txt";

            calculator.CalculateAsync(maxNumber, filePath);




            Console.ReadKey();
            return;
        }
    }
}
