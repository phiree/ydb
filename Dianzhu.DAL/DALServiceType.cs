using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALServiceType : NHRepositoryBase<ServiceType,Guid>,IDAL.IDALServiceType
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
            return Session.QueryOver<ServiceType>().Where(x => x.DeepLevel == level).And(x => x.Name == name).SingleOrDefault();
        }

        public void SaveList(IList<ServiceType> typeList)
        {
            foreach (ServiceType type in typeList)
            {
                Session.SaveOrUpdate(type);
            }
        }
    }
}
