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
        ISession session;
        IRepositoryServiceTypePoint repositoryServiceTypePoint;
        internal ServiceTypePointService(ISession session, IRepositoryServiceTypePoint repositoryServiceTypePoint)
        {
            this.session = session;
            this.repositoryServiceTypePoint = repositoryServiceTypePoint;
        }

        public void Add(string serviceType, decimal point)
        {
            ServiceTypePoint stp = new ServiceTypePoint { ServiceTypeId = serviceType, Point = point };
            repositoryServiceTypePoint.Add(stp);
        }

        string errmsg;
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
        public IList<ServiceTypePointDto> GetAll()
        {
            return Mapper.Map<IList<ServiceTypePoint>, IList<ServiceTypePointDto>>(repositoryServiceTypePoint.GetAll());
        }
    }
}
