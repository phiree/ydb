using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using Dianzhu.IDAL;
namespace Dianzhu.DAL
{
    public class DALArea :NHRepositoryBase<Area,int>,IDALArea
    {
    }
}
