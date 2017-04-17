using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
