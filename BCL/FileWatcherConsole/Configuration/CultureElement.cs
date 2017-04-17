using System.Configuration;

namespace FileWatcherConsole.Configuration
{
    public class CultureElement : ConfigurationElement
    {
        [ConfigurationProperty("value")]
        public string Value
        {
            get { return ((string)(base["value"])); }
        }
    }
}
