using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.IDAL
{
    public interface IDALDZService
    {
        IDALBase<DZService> DalBase { get; set; }
        IList<DZService> GetList(Guid businessId, Guid serviceTypeId, int pageindex, int pagesize, out int totalRecord);    
    }
}
