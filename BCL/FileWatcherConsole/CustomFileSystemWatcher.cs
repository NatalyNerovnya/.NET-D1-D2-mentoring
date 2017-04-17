using FileWatcherConsole.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using System.Resources;
using logginng = FileWatcherConsole.Resources.Log;
using System.Globalization;

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

        public CustomFileSystemWatcher(CustomConfigSection config)
        {
            watcher = new FileSystemWatcher();
            this.config = config;
            var path = config.Folder.Path;


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Changed += new FileSystemEventHandler(OnAdd);
            watcher.Created += new FileSystemEventHandler(OnAdd);
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
        }


        private void OnAdd(object source, FileSystemEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(config.CultulreInfo.Value);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(config.CultulreInfo.Value);

            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                Console.WriteLine(logginng.NewFile);
            }
            var rules = config.RuleItems;
            bool isMatch = false;
            for (int i = 0; i < rules.Count; i++)
            {
                var reg = new Regex(rules[i].NameTemplate, RegexOptions.IgnoreCase);
                if (reg.IsMatch(e.Name))
                {
                    var dateFormat = rules[i].DateFormat;
                    MoveToFolder(e.Name, rules[i].Folder, dateFormat);
                    isMatch = true;
                    Console.WriteLine(logginng.FindedRule);
                    break;
                }
            }

            if (!isMatch)
            {
                Console.WriteLine(logginng.NotFindedRule);
                MoveToFolder(e.Name, config.DefaultFolder.Path, null);
            }
        }

        private void MoveToFolder(string file, string folder, string dateFormat)
        {
            string date;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            if(dateFormat == null)
            {
                date = DateTime.Now.ToShortDateString().ToString(CultureInfo.CreateSpecificCulture(config.CultulreInfo.Value));
            }
            else
            {
                date = DateTime.Now.ToString(dateFormat);
            }
            var sourcePath = System.IO.Path.Combine(Path, file);
            var targetPath = System.IO.Path.Combine(folder, date + file);
            Directory.Move(sourcePath, targetPath);
            Console.WriteLine("{0} {1}", logginng.Move, folder);
        }
    }
}
