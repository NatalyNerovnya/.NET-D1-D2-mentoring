using System.Configuration;

namespace FileWatcherConsole.Configuration
{
    public class FolderElement : ConfigurationElement
    {
        [ConfigurationProperty("path")]
        public string Path
        {
            get { return ((string)(base["path"])); }
        }
    }
}
