using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FileWatcherConsole.Configuration;
using System.Threading;

namespace FileWatcherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var conf = (CustomConfigSection)ConfigurationManager.GetSection("CustomSection");
            var fsw = new CustomFileSystemWatcher(conf);

            while (true)
            {
                fsw.Start();
                Thread.Sleep(500);
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    fsw.Stop();
                    break;
                }
            }

        }
    }
}
