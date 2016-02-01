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
    public class DALOrderAssignment : DALBase<OrderAssignment>
    {
        public DALOrderAssignment()
        {

        }
        //注入依赖,供测试使用;
        public DALOrderAssignment(string fortest) : base(fortest)
        {

        }
        
    }
}
