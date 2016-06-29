using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Dianzhu.DAL.Client
{
    public class DALUserToken : Dianzhu.DAL.NHRepositoryBase<Model.UserToken, Guid>, IDAL.IDALUserToken
    {
    }
}
