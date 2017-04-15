using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FileWatcherConsole.Configuration
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("nameTemplate", IsKey = true)]
        public string NameTemplate
        {
            get { return ((string)(base["nameTemplate"])); }
        }

        [ConfigurationProperty("path", IsKey = false)]
        public string Folder
        {
            get { return ((string)(base["path"])); }
        }
    }
}
