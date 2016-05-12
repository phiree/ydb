using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DDDCommon;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    /// <summary>
    /// nhibernate implenmenting
    /// </summary>
    public class DALAdvertisement:NHRepositoryBase<Advertisement,Guid>, Dianzhu.DAL.IDALAdvertisement
    {
        
    }
}
