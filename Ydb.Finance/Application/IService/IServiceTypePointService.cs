using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public interface IServiceTypePointService
    {
        /// <summary>
        /// 添加一条服务类型扣点比例
        /// </summary>
        /// <param name="serviceTypeId" type="string">服务类型ID</param>
        /// <param name="point" type="decimal">扣点比例</param>
        void Add(string serviceType, decimal point);

        /// <summary>
        /// 修改一条服务类型扣点比例
        /// </summary>
        /// <param name="serviceTypeId" type="string">服务类型ID</param>
        /// <param name="point" type="decimal">扣点比例</param>
        void Update(string serviceTypeId, decimal point);

        /// <summary>
        /// 根据服务类型ID获取扣点比例
        /// </summary>
        /// <param name="serviceTypeId" type="string">服务类型ID</param>
        /// <returns type="decimal">扣点比例</returns>
        decimal GetPoint(string serviceTypeId);

        /// <summary>
        /// 根据服务类型ID获取扣点比例信息
        /// </summary>
        /// <param name="serviceTypeId" type="string">服务类型ID</param>
        /// <returns type="ServiceTypePointDto">扣点比例信息</returns>
        ServiceTypePointDto GetPointInfo(string serviceTypeId);

        /// <summary>
        /// 获取所有的服务类型扣点比例
        /// </summary>
        /// <returns type="IList<ServiceTypePointDto>">服务类型扣点比例信息列表</returns>
        IList<ServiceTypePointDto> GetAll();

        /// <summary>
        /// 批量保存服务类型扣点比例
        /// </summary>
        /// <param name="serviceTypePointDtoList" type="IList<ServiceTypePointDto>">服务类型扣点比例列表</param>
        void SaveList(IList<ServiceTypePointDto> serviceTypePointDtoList);
    }
}
