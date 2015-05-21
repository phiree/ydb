using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALServiceType : IDAL.IDALServiceType
    {
        IDAL.IDALBase<ServiceType> dalBase = null;
        public IDAL.IDALBase<ServiceType> DalBase
        {
            get { return new DalBase<ServiceType>(); }
            set { dalBase = value; }
        }
        

         
    }
}
