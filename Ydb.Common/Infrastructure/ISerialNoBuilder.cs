using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Common.Infrastructure
{
    public interface ISerialNoBuilder
    {
        string GetSerialNo(string key, int serialNoLength);
    }
}
