using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.BusinessResource.DomainModel;

using NHibernate;

namespace Ydb.BusinessResource.Infrastructure.Repository
{
    public class RepositoryServiceType : NHRepositoryBase<ServiceType, Guid>, IRepositoryServiceType
    {
         

        public IList<ServiceType> GetTopList()
        {
            return Find(x=>x.Parent==null);
        }
        public ServiceType GetOneByCode(string code)
        {
            ServiceType s = FindOne(x => x.Code == code); 
            return s;
        }

        public ServiceType GetOneByName(string name, int level)
        {
            return session.QueryOver<ServiceType>().Where(x => x.DeepLevel == level).And(x => x.Name == name).SingleOrDefault();
        }

        public void SaveList(IList<ServiceType> typeList)
        {
            foreach (ServiceType type in typeList)
            {
                session.SaveOrUpdate(type);
            }
        }

        public void DeleteAll()
        {
            session.Delete("from ServiceType t");
        }
    }
}
