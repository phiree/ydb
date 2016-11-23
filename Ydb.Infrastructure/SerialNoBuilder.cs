using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Infrastructure;
using Ydb.Common.Domain;

namespace Ydb.Infrastructure
{
    public class SerialNoBuilder: NHRepositoryBase<SerialNo, Guid>, ISerialNoBuilder
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.Infrastructure.SerialNoBuilder");
        public string GetSerialNo(string key, int serialNoLength)
        {
            var list = Find(x => x.SerialKey == key);
            if (list.Count == 1)
            {
                var currentS = list[0];
                currentS.SerialValue += 1;
                return currentS.BuildFormatedNo(serialNoLength);
            }
            else if (list.Count == 0)
            {
                SerialNo s = new SerialNo { SerialKey = key, SerialValue = 1 };
                Add(s);
                return s.BuildFormatedNo(serialNoLength);
            }
            else
            {
                string err = "SerialNo Has Dumplicate Key";
                log.Error(err);
                throw new Exception(err);
            }

        }
    }
}
