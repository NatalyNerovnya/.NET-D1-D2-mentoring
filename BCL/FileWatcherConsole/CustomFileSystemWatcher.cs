using FileWatcherConsole.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcherConsole
{
    public class CustomFileSystemWatcher
    {
        private FileSystemWatcher watcher;
        private CustomConfigSection config;

        public string Path
        {
            get
            {
                return watcher.Path;
            }
        }

        public CustomFileSystemWatcher()
        {
            watcher = new FileSystemWatcher();
            config = (CustomConfigSection)ConfigurationManager.GetSection("CustomSection");
            //check folder
            watcher.Path = config.Folder.Path;
        }

        // Define the event handlers.
        private static void OnAdd(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }
    }
}
