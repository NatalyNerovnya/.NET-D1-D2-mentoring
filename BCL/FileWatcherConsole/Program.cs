using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FileWatcherConsole.Configuration;
using System.Threading;
using System.Globalization;

namespace FileWatcherConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var conf = (CustomConfigSection)ConfigurationManager.GetSection("CustomSection");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
            var fsw = new CustomFileSystemWatcher(conf);
            fsw.Start();

            while (true)
            {                
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
