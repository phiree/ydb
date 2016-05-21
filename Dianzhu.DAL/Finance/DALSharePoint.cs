using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Finance;
namespace Dianzhu.DAL.Finance
{
   public  class DALSharePoint:DALBase<Model.Finance.SharePoint>
    {
        
        public DALSharePoint(string fortest) : base(fortest) { }
        public DALSharePoint() { }

        public SharePoint GetSharePoint(Model.DZMembership membership)
        {
            string query = "select s from Sharepoint s inner join s.Membership m where m.Id='" + membership.Id + "'";
            SharePoint settedPoint= GetOneByQuery(query);
            return settedPoint;
        }
    }
}
