using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALBusiness : IDAL.IDALBusiness
    {
        IDAL.IDALBase<Business> dalBase = null;
        public IDAL.IDALBase<Business> DalBase
        {
            get {return new DalBase<Business>(); }
            set { dalBase = value; }
        }

        public void CreateBusinessAndUser(string code)
        {
            throw new NotImplementedException();
        }
    }
}
