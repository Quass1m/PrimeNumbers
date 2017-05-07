using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PrimeNumbers
{
    class Calculator
    {
        public void Calculate(long maxNumber, string filePath)
        {
            Console.WriteLine($"Calculate prime numbers up to {maxNumber}");
            var watch = new Stopwatch();

            watch.Start();
            using (var stream = new FileStream(filePath,FileMode.Create))
            {
                for (long i = 2; i <= maxNumber; i++)
                {
                    if (CheckIfPrime(i))
                        AddNumber(stream, i);
                }
            }
            watch.Stop();

            Console.WriteLine($"Calculations finished in {watch.Elapsed.TotalSeconds}s. Files written to: {filePath}");
        }

        private static void AddNumber(FileStream fs, long value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value.ToString() + "\r\n");
            fs.WriteAsync(info, 0, info.Length);
        }

        private bool CheckIfPrime(long number)
        {
            if (number == 0)
                return false;

            for (long i = 2; i <= number/2 ; i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }
}
