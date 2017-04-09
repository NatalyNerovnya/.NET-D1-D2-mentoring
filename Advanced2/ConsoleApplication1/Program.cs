using System;
using FileSystem;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirName = @"C:\";
            var fVisitr = new FileSystemVisitor(@"D:\", (s1, s2) => string.Compare(s2, s1, StringComparison.Ordinal));
            foreach (var file in fVisitr)
            {
                Console.WriteLine(file);
            }
            Console.ReadLine();
        }
    }
}
