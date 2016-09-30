using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL.Common.SerialNo
{
    public interface ISerialNoBuilder
    {
          string GetSerialNo(string key,int serialNoLength);
    }
}
