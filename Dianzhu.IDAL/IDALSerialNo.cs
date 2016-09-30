using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.IDAL
{
    public interface IDALSerialNo:IRepository<Model.SerialNo,Guid>
    {
        IList<Dianzhu.Model.SerialNo> FindBySerialKey(string serialKey);
    }
}
