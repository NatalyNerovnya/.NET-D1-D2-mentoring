using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FileSystemWatcherWrapper
{
    public class FolderElement : ConfigurationElement
    {
        [ConfigurationProperty("Folder")]
        public string Path
        {
            get { return ((string)(base["path"])); }
        }
    }
}
