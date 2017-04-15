using FileWatcherConsole.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
            var path = config.Folder.Path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
           | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Changed += new FileSystemEventHandler(OnAdd);
            watcher.EnableRaisingEvents = true;
        }

        private static void OnAdd(object source, FileSystemEventArgs e)
        {
            CustomFileSystemWatcher reference = source as  CustomFileSystemWatcher;
            var rules = reference.config.RuleItems;
            bool isMatch = false;
            for (int i = 0; i < rules.Count; i++)
            {
                var reg = new Regex(rules[i].NameTemplate, RegexOptions.IgnoreCase);
                if (reg.IsMatch(e.Name))
                {
                    reference.MoveToFolder(e.Name, rules[i].Folder);
                    isMatch = true;
                    i = rules.Count; //Not beautiful exit from for loop
                }
            }

            if (!isMatch)
            {
                reference.MoveToFolder(e.Name, "123"); //TODO: store in resource file
            }
        }

        private void MoveToFolder(string file, string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var sourcePath = System.IO.Path.Combine(Path, file);
            var targetPath = System.IO.Path.Combine(folder, file);
            Directory.Move(sourcePath, targetPath);
        }
    }
}
