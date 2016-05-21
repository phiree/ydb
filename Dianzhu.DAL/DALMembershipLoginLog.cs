using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

namespace Dianzhu.DAL
{
    public class DALMembershipLoginLog : DALBase<Model.MembershipLoginLog>
    {
        
        public DALMembershipLoginLog()
        {
           
        }
        //注入依赖,供测试使用;
        public DALMembershipLoginLog(string fortest) : base(fortest)
        {
            
        }
       
       
    }
}
