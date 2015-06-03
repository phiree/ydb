using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALBusinessImage : IDAL.IDALBusinessImage
    {
        IDAL.IDALBase<BusinessImage> dalBase = null;
        public IDAL.IDALBase<BusinessImage> DalBase
        {
            get { return new DalBase<BusinessImage>(); }
            set { dalBase = value; }
        }

      
    }
}
