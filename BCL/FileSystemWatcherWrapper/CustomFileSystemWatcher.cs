using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FileSystemWatcherWrapper
{
    public class CustomFileSystemWatcher
    {
        private FileSystemWatcher watcher;
        private CustomConfigSection config;

        public CustomFileSystemWatcher()
        {
            config = (CustomConfigSection)ConfigurationManager.GetSection("configuration/CustomSection");
            watcher = new FileSystemWatcher();
            watcher.Path = config.Folder.Path;
        }
    }
}
