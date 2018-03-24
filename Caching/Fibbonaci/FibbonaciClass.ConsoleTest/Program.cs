namespace FibbonaciClass.ConsoleTest
{
    using System;
    using System.Diagnostics;

    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            Console.WriteLine("With System.Runtime.Caching ");
            timer.Start();
            Console.WriteLine(FibbonaciNumbers.Generate(7));
            timer.Stop();
            Console.WriteLine($"Ticks : {timer.ElapsedTicks}");
            Console.WriteLine();

            FibbonaciNumbers.Cacher = new InRedisCache();

            Console.WriteLine("With Redis");
            timer.Restart();
            Console.WriteLine(FibbonaciNumbers.Generate(7));
            timer.Stop();
            Console.WriteLine($"Ticks : {timer.ElapsedTicks}");
            Console.ReadLine();
        }
    }
}
