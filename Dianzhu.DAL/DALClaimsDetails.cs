using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALClaimsDetails : NHRepositoryBase<ClaimsDetails, Guid>, IDAL.IDALClaimsDetails
    {

    }
}
