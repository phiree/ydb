using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.BusinessResource.DomainModel;

using NHibernate;
using NHibernate.Transform;

namespace Ydb.BusinessResource.Infrastructure.YdbNHibernate.Repository
{
    public class RepositoryServiceType : NHRepositoryBase<ServiceType, Guid>, IRepositoryServiceType
    {
         

        public IList<ServiceType> GetTopList()
        {
            // return session.CreateQuery("select t from ServiceType t join fetch t.Children c1 join fetch c1.Children c2").SetResultTransformer(new DistinctRootEntityResultTransformer()).List<ServiceType>();
            return session.QueryOver<ServiceType>().Fetch(x => x.Children).Eager .List().Distinct<ServiceType>().ToList();
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
