﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FileWatcherConsole.Configuration
{
    public class DefaultFolderElement : ConfigurationElement
    {
        [ConfigurationProperty("path")]
        public string Path
        {
            get { return ((string)(base["path"])); }
        }
    }
}
