using System.Configuration;

namespace Dianzhu.Web.RestfulApi
{
    public class MySectionCollection:ConfigurationSection
    {
        private static readonly ConfigurationProperty property = new ConfigurationProperty(string.Empty, typeof(MySectionKeyValue), null, ConfigurationPropertyOptions.IsDefaultCollection);

        [ConfigurationProperty("",Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public MySectionKeyValue KeyValues
        {
            get { return (MySectionKeyValue)base[property]; }
        }
    }
}
