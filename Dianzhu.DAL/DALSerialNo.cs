using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALSerialNo : NHRepositoryBase<SerialNo, Guid>, IDAL.IDALSerialNo
    {
        public IList<SerialNo> FindBySerialKey(string serialKey)
        {
            return Find(x => x.SerialKey == serialKey);
        }
    }
}
