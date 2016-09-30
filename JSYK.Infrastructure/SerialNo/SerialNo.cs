using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL.Common.SerialNo;
using Dianzhu.IDAL;
using Model= Dianzhu.Model;
namespace JSYK.Infrastructure.SerialNo
{
    public class SerialNoDb : ISerialNoBuilder
    {
        log4net.ILog log = log4net.LogManager.GetLogger("JSYK.Infrastructure.SerialNo");
        IDALSerialNo dalSerialNo;
        public SerialNoDb(IDALSerialNo dalSerialNo)
        {
            this.dalSerialNo = dalSerialNo;
        }
        public string GetSerialNo(string key,int serialNoLength)
        {
           var list= dalSerialNo.FindBySerialKey(key);
            if (list.Count == 1)
            {
                var currentS = list[0];
                
                currentS.SerialValue += 1;
                return currentS.BuildFormatedNo(serialNoLength);
            }
            else if (list.Count == 0)
            {
                Model.SerialNo s = new Dianzhu.Model.SerialNo { SerialKey = key, SerialValue = 1 };
                dalSerialNo.Add(s);
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
