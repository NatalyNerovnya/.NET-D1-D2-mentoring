using System;
using System.Collections.Generic;
using System.IO;
using FileSystem;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirName = @"C:\";
            var fVisitr = new FileSystemVisitor(@"D:\");
            foreach (var file in fVisitr)
            {
                Console.WriteLine(file);
            }
            Console.ReadLine();
        }
    }
}
