using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FileWatcherConsole
{
    [ConfigurationCollection(typeof(RuleElement))]
    public class RulesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)(element)).NameTemplate;
        }

        public RuleElement this[int idx]
        {
            get { return (RuleElement)BaseGet(idx); }
        }
    }
}
