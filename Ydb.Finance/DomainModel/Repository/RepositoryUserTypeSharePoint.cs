using System;
using System.Text;
using System.Collections;
using Ydb.Common.Repository;
using Ydb.Finance.DomainModel;
using Ydb.Finance.DomainModel.Enums;
namespace Ydb.Finance.DomainModel
{
    internal interface IRepositoryUserTypeSharePoint :IRepository< UserTypeSharePoint,Guid>
    {
        /// <summary>
        /// 根据用户类型获取用户类型分配比例信息
        /// </summary>
        /// <param name="userType" type="UserType">用户类型</param>
        /// <returns type="UserTypeSharePoint">用户类型分配比例信息</returns>
        UserTypeSharePoint Add(UserType userType, decimal point);

        /// <summary>
        /// 新增用户类型分配比例信息
        /// </summary>
        /// <param name="userType" type="UserType"></param>
        /// <param name="point" type="point"></param>
        /// <returns type="UserTypeSharePoint">用户类型分配比例信息</returns>
        UserTypeSharePoint GetSharePoint(UserType userType);
        
    }
}
