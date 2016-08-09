using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Finance;
namespace Dianzhu.IDAL.Finance
{
    public interface IDALSharePoint:IRepository<Dianzhu.Model.Finance.SharePoint,Guid>
    {

          SharePoint GetSharePoint(Model.DZMembership membership);
    }
}
