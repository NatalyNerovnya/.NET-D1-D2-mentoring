using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileSystemWatcherWrapper;
using System.Configuration;

namespace FileWatcherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var conf = (CustomConfigSection)ConfigurationManager.GetSection("CustomSection");
            var rules = conf.RuleItems;
            var folder = conf.Folder.Path;
        }
    }
}
