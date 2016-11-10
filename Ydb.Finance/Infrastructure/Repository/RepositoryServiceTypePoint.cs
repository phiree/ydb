using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using NHibernate;
using Ydb.Common.Repository;
namespace Ydb.Finance.Infrastructure.Repository
{
   public class RepositoryServiceTypePoint : NHRepositoryBase<ServiceTypePoint, Guid>, IRepositoryServiceTypePoint
    {
       
        /// <summary>
        /// 根据服务ID获取服务扣点比例信息
        /// </summary>
        /// <param name="serviceTypeId" type="string">服务类型ID</param>
        /// <returns type="ServiceTypePoint">服务扣点比例信息</returns>
        public ServiceTypePoint GetOneByServiceType(string serviceTypeId)
        {
            var result = FindOne(x => x.ServiceTypeId == serviceTypeId);
            return result;
        }

        /// <summary>
        /// 获取所有的服务扣点比例信息列表
        /// </summary>
        /// <returns type="IList<ServiceTypePoint>">服务扣点比例信息列表</returns>
        public IList<ServiceTypePoint> GetAll()
        {
            return Find(x => true);
        }

        /// <summary>
        /// 批量更新服务扣点比例
        /// </summary>
        /// <param name="list" type="IList<ServiceTypePoint>">服务扣点比例信息列表</param>
        public void SaveList(IList<ServiceTypePoint> list)
        {
            foreach (ServiceTypePoint item in list)
            {
                SaveOrUpdate(item);
            }
        }
    }
}
