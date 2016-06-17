using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

namespace Dianzhu.DAL
{
    public class DALMembershipLoginLog : NHRepositoryBase<Model.MembershipLoginLog,Guid>,IDAL.IDALMembershipLoginLog
    {
       
    }
}
