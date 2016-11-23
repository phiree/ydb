using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using FluentValidation;
using FluentValidation.Results;
using System.Collections;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;

namespace Ydb.BusinessResource.Application
{
    /// <summary>
    /// 服务时间段业务逻辑
    /// </summary>
    [Obsolete("valueobject不应该公布服务")]
    public class ServiceOpenTimeService : IServiceOpenTimeService
    {
        //20160617_longphui_modify
        //public DALServiceOpenTime DALServiceOpenTime = null;
        IRepositoryServiceOpenTime repositoryServiceOpenTime = null;
        public ServiceOpenTimeService(IRepositoryServiceOpenTime repositoryServiceOpenTime)
        {
            //DALServiceOpenTime = DALFactory.DALServiceOpenTime;
            this.repositoryServiceOpenTime = repositoryServiceOpenTime;
        }
        public ServiceOpenTime GetOne( Guid id  )
        {
            //return DALServiceOpenTime.GetOne(id);
            return repositoryServiceOpenTime.FindById(id);
        }
        
        public void Update(ServiceOpenTime sot)
        {
            //DALServiceOpenTime.SaveOrUpdate(sot);
            repositoryServiceOpenTime.Update(sot);
        }
        
    }
    /// <summary>
    /// 服务时间段业务逻辑
    /// </summary>
    [Obsolete("valueobject不应该公布服务")]
    public class  ServiceOpenTimeForDayService : IServiceOpenTimeForDayService
    {
        IRepositoryServiceOpenTimeForDay repositoryServiceOpenTimeForDay;
        public ServiceOpenTimeForDayService(IRepositoryServiceOpenTimeForDay repositoryServiceOpenTimeForDay)
        {
            this.repositoryServiceOpenTimeForDay = repositoryServiceOpenTimeForDay;
        }
        //public BLLServiceOpenTimeForDay(DALServiceOpenTimeForDay dal)
        //{
        //    DALServiceOpenTimeForDay = dal;
        //}
        public ServiceOpenTimeForDay GetOne(Guid id)
        {
            return repositoryServiceOpenTimeForDay.FindById(id);
        }
        public void Delete(ServiceOpenTimeForDay sotForDay)
        {
            repositoryServiceOpenTimeForDay.Delete(sotForDay);
        }
        
        public void Update(ServiceOpenTimeForDay sotForDay)
        {
            //todo: 这里需要判断 该时间段内是否已经有了订单, 如果有了 则不能删除.
            repositoryServiceOpenTimeForDay.Update(sotForDay);
        }

    }
}
