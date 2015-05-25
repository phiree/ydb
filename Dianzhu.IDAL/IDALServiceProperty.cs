using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.IDAL
{
    public interface IDALServiceProperty
    {
        IDALBase<ServiceProperty> DalBase { get; set; }
        IList<ServiceProperty> GetList(Guid serviceTypeId);
        
    }
}
