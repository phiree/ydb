using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALAdvertisement : DALBase<Advertisement>
    {
         public DALAdvertisement()
        {
             
        }
        //注入依赖,供测试使用;
         public DALAdvertisement(string fortest):base(fortest)
        {
            
        }
        
        public IList <Advertisement> GetADList()
        {
            return Session.QueryOver<Advertisement>().OrderBy(x => x.Num).Asc.List();
        }
    }
}
