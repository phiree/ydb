using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class SerialNoMap : ClassMap<SerialNo>
    {
        public SerialNoMap() {
            Id(x => x.Id);
            Map(x => x.SerialKey);
            Map(x => x.SerialValue);
           
        }
    }
}
