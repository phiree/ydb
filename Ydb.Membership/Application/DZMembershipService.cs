using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
namespace Ydb.Membership.Application
{
  public class DZMembershipService:IDZMembershipService
    {
        ISession session;
        public DZMembershipService(ISession session)
        {
            this.session = session;
        }
        public bool RegisterBusinessUser(string registerName, string password, out string errMsg)
        {
            errMsg = string.Empty;


            return true;

        }
        
           


    }
}
