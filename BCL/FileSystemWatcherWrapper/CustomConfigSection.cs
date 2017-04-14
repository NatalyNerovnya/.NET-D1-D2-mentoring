using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FileSystemWatcherWrapper
{
    public class CustomConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Rules")]
        public RulesCollection RuleItems
        {
            get { return ((RulesCollection)(base["Rules"])); }
        }

        [ConfigurationProperty("Folder")]
        public FolderElement Folder
        {
            get { return ((FolderElement)(base["Folder"])); }
        }
    }
}
