using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Finance;
namespace Dianzhu.DAL.Finance
{
   public  class DALSharePoint:NHRepositoryBase<SharePoint,Guid>,IDAL.Finance.IDALSharePoint
    {
        
       

        public SharePoint GetSharePoint(Model.DZMembership membership)
        {
             

            return FindOne(x => x.Membership.Id == membership.Id);
        }
    }
}
