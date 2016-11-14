using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using Ydb.Finance.DomainModel.Enums;
namespace Ydb.Finance.Application
{
   public interface IUserTypeSharePointService
    {
        /// <summary>
        /// 新增用户类型分配比例信息
        /// </summary>
        /// <param name="userType" type="string">用户类型</param>
        /// <param name="point" type="decimal">分成比例</param>
        void Add(string userType,decimal sharepoint);

        /// <summary>
        /// 修改用户类型分配比例信息
        /// </summary>
        /// <param name="userType" type="string">用户类型</param>
        /// <param name="point" type="decimal">分成比例</param>
        void Update(string userType, decimal point);

        /// <summary>
        /// 根据用户类型获取用户类型分配比例
        /// </summary>
        /// <param name="userType" type="string">用户类型</param>
        /// <returns type="decimal">用户类型分配比例</returns>
        decimal GetSharePoint(string userType,out string errMsg);

        /// <summary>
        /// 根据用户类型获取用户类型分配比例信息
        /// </summary>
        /// <param name="userType" type="string">用户类型</param>
        /// <returns type="UserTypeSharePoint">用户类型分配比例信息</returns>
        UserTypeSharePointDto GetSharePointInfo(string userType);

        /// <summary>
        /// 获取所有用户类型分配比例信息
        /// </summary>
        /// <returns type="IList<UserTypeSharePointDto>">用户类型分配比例信息列表</returns>
        IList<UserTypeSharePointDto> GetAll();


    }
}
