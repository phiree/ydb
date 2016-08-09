using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using Dianzhu.IDAL;

namespace Dianzhu.DAL
{
 
    public class DALComplaint : NHRepositoryBase<Complaint,Guid>,IDAL.IDALComplaint
 
 
    {

    }
}
