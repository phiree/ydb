using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using NHibernate;
using NHibernate.Criterion;
namespace Dianzhu.DAL
{
    public class DALServiceOrderAppraise : DALBase<ServiceOrderAppraise>
    {
        public DALServiceOrderAppraise()
        {

        }
        //注入依赖,供测试使用;
        public DALServiceOrderAppraise(string fortest) : base(fortest)
        {

        }

        
    }
}
