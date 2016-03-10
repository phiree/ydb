using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALBusinessImage : DALBase<BusinessImage>
    {
         
         public DALBusinessImage()
        {
             
        }
        //注入依赖,供测试使用;
         public DALBusinessImage(string fortest):base(fortest)
        {
            
        }

        public BusinessImage FindBusImageByName(string imgName)
        {
            return Session.QueryOver<BusinessImage>().Where(x => x.ImageName == imgName).SingleOrDefault();
        }
      
    }
}
