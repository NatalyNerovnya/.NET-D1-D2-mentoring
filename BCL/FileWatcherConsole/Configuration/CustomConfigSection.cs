using System.Configuration;

namespace FileWatcherConsole.Configuration
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

        [ConfigurationProperty("DefaultFolder")]
        public DefaultFolderElement DefaultFolder
        {
            get { return ((DefaultFolderElement)(base["DefaultFolder"])); }
        }

        [ConfigurationProperty("Culture")]
        public CultureElement CultulreInfo
        {
            get { return ((CultureElement)(base["Culture"])); }
        }
    }
}
