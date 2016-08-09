using System.Configuration;
using System;

namespace Dianzhu.Web.RestfulApi
{
    [ConfigurationCollection(typeof(MySectionKeyValueSettings))]
    public class MySectionKeyValue : ConfigurationElementCollection
    {
        public MySectionKeyValue()
            : base(StringComparer.OrdinalIgnoreCase)  //忽略大小写
        {

        }

        //其实关键就是这个索引器，但它也是调用基类的实现，只是做下类型转换就行了
        new public MySectionKeyValueSettings this[string name]
        {
            get
            {
                return (MySectionKeyValueSettings)base.BaseGet(name);
            }
        }

        //下面二个方法中抽象类中必须要实现的
        protected override ConfigurationElement CreateNewElement()
        {
            return new MySectionKeyValueSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MySectionKeyValueSettings)element).Key;
        }

        //说明：如果不需要在代码中修改集合，可以不实现Add,Clear,Remove
        public void Add(MySectionKeyValueSettings setting)
        {
            this.BaseAdd(setting);
        }

        public void Clear()
        {
            this.BaseClear();
        }

        public void Get(MySectionKeyValueSettings setting)
        {
            this.BaseGet(setting.Key);
        }

        public void Remove(string name)
        {
            base.BaseRemove(name);
        }
    }
}
