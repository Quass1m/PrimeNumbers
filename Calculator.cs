using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrimeNumbers
{
    class Calculator
    {
        public async void CalculateAsync(long maxNumber, string filePath)
        {
            Console.WriteLine($"Calculate prime numbers up to {maxNumber}");
            var watch = new Stopwatch();

            Func<long, bool> foo = number =>
            {
                if (number == 0)
                    return false;

                for (long i = 2; i <= number / 2; i++)
                {
                    if (number % i == 0)
                        return false;
                }

                return true;
            };

            watch.Start();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                for (long i = 2; i <= maxNumber; i++)
                {
                    var perNumberTime = new Stopwatch();
                    perNumberTime.Start();
                    bool isPrime = IsPrime(i);
                    perNumberTime.Stop();

                    if (isPrime)
                        AddNumber(stream, i, 0);
                }

                //IEnumerable<int> range = Enumerable.Range(2, (int)maxNumber);

                //foreach(int i in range.Where(n => foo(n)))
                //{
                //    AddNumber(stream, i, 0);
                //}
            }
            watch.Stop();

            Console.WriteLine($"Calculations finished in {watch.Elapsed.TotalSeconds}s. Files written to: {filePath}");
        }

        private static void AddNumber(FileStream fs, long value, long ticks)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value.ToString() + "\t" + ticks + " ticks" + "\r\n");
            fs.WriteAsync(info, 0, info.Length);
        }

        private static Task<bool> IsPrime(long number)
        {
            if (number == 0)
                return new Task<bool>(() => false);

            for (long i = 2; i <= number / 2; i++)
            {
                if (number % i == 0)
                    return new Task<bool>(() => false);
            }

            return new Task<bool>(() => true);
        }
    }
}
