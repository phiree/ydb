using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALStaff : IDAL.IDALStaff
    {
        IDAL.IDALBase<Staff> dalBase = null;
        public IDAL.IDALBase<Staff> DalBase
        {
            get { return new DalBase<Staff>(); }
            set { dalBase = value; }
        }

        
    }
}
