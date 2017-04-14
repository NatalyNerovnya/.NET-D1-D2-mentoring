using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemWatcherWrapper;

namespace FileWatcherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var systemWatcher = new CustomFileSystemWatcher();
        }
    }
}
