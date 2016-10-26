using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using NHibernate;
using Ydb.Finance.DomainModel.Enums;
using AutoMapper;

namespace Ydb.Finance.Application
{
    public class ServiceTypePointService: IServiceTypePointService
    {
        IRepositoryServiceTypePoint repositoryServiceTypePoint;
        internal ServiceTypePointService(IRepositoryServiceTypePoint repositoryServiceTypePoint)
        {
            this.repositoryServiceTypePoint = repositoryServiceTypePoint;
        }

        /// <summary>
        /// 添加一条服务类型扣点比例
        /// </summary>
        /// <param name="serviceTypeId" type="string">服务类型ID</param>
        /// <param name="point" type="decimal">扣点比例</param>
        [Ydb.Common.Repository.UnitOfWork]
        public void Add(string serviceTypeId, decimal point)
        {
            ServiceTypePoint stp = new ServiceTypePoint { ServiceTypeId = serviceTypeId, Point = point };
            repositoryServiceTypePoint.Add(stp);
        }

        /// <summary>
        /// 根据服务类型ID获取扣点比例
        /// </summary>
        /// <param name="serviceTypeId" type="string">服务类型ID</param>
        /// <returns type="decimal">扣点比例</returns>
        [Ydb.Common.Repository.UnitOfWork]
        public decimal GetPoint(string serviceTypeId)
        {
            var serviceTypePoint = repositoryServiceTypePoint.GetOneByServiceType(serviceTypeId);
            if (serviceTypePoint == null)
            {
                //if (serviceType.Parent != null)
                //{
                //    return GetPoint(serviceType.Parent);
                //}
                //else
                //{
                //    errmsg = "该服务类型的扣点比例未设置:" + serviceType.Name + "(" + serviceType.Id + ")";
                //    log.Error(errmsg);
                //    throw new Exception("该服务类型的扣点比例未设置:" + serviceType.Name);
                //}
                throw new Exception("该服务类型的扣点比例未设置!");
            }
            else
            {
                return serviceTypePoint.Point;
            }
        }

        /// <summary>
        /// 获取所有的服务类型扣点比例
        /// </summary>
        /// <returns type="IList<ServiceTypePointDto>">服务类型扣点比例信息列表</returns>
        [Ydb.Common.Repository.UnitOfWork]
        public IList<ServiceTypePointDto> GetAll()
        {
            return Mapper.Map<IList<ServiceTypePoint>, IList<ServiceTypePointDto>>(repositoryServiceTypePoint.GetAll());
        }
    }
}
