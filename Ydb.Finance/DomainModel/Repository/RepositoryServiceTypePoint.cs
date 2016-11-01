using System;
using System.Text;
using System.Collections;
using Ydb.Common.Repository;
using Ydb.Finance.DomainModel;
using System.Collections.Generic;

namespace Ydb.Finance.DomainModel
{
    internal interface IRepositoryServiceTypePoint : IRepository< ServiceTypePoint,Guid>
    {
        /// <summary>
        /// 根据服务ID获取服务扣点比例信息
        /// </summary>
        /// <param name="serviceTypeId" type="string">服务类型ID</param>
        /// <returns type="ServiceTypePoint">服务扣点比例信息</returns>
        ServiceTypePoint GetOneByServiceType(string serviceTypeId);

        /// <summary>
        /// 获取所有的服务扣点比例信息列表
        /// </summary>
        /// <returns type="IList<ServiceTypePoint>">服务扣点比例信息列表</returns>
        IList<ServiceTypePoint> GetAll();

        /// <summary>
        /// 批量更新服务扣点比例
        /// </summary>
        /// <param name="list" type="IList<ServiceTypePoint>">服务扣点比例信息列表</param>
        void SaveList(IList<ServiceTypePoint> list);
    }
}
