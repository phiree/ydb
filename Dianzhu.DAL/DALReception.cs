using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALReception : DALBase<Model.ReceptionBase>
    {
         public DALReception()
        {
            Session = new HybridSessionBuilder().GetSession();
        }
        //注入依赖,供测试使用;
         public DALReception(string fortest)
        {
            
        }
        
        public IList<ReceptionBase> Search(DZMembership from,DZMembership to, DateTime timeBegin, DateTime timeEnd,int limit)
        {

            var result = Session.QueryOver<ReceptionBase>().Where(x => x.TimeBegin >= timeBegin)
                 .And(x => x.TimeBegin <= timeEnd);
           if (from!=null)
           {
               result = result.And(x => x.Sender == from);
           }
           if (to!=null)
           {
               result = result.And(x => x.Receiver == to);
           }
            result.OrderBy(x => x.TimeBegin).Desc.Take(limit). List();
           return result.List();
        }
    }
}
