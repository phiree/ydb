using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALBusinessImage : NHRepositoryBase<BusinessImage, Guid>,IDAL.IDALBusinessImage
    {
         
       

        public BusinessImage FindBusImageByName(string imgName)
        {
            return FindOne(x => x.ImageName == imgName);
            
        }
      
    }
}
