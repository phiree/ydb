using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Ydb.Common.Repository;
using Ydb.Finance.DomainModel;
using NHibernate.Transform;
using Ydb.Finance.DomainModel.Enums;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Ydb.Finance.Infrastructure.Repository
{
    internal class RepositoryUserTypeSharePoint : NHRepositoryBase<UserTypeSharePoint, Guid>, IRepositoryUserTypeSharePoint
    {
        
        /// <summary>
        /// 根据用户类型获取用户类型分配比例信息
        /// </summary>
        /// <param name="userType" type="UserType">用户类型</param>
        /// <returns type="UserTypeSharePoint">用户类型分配比例信息</returns>
        public UserTypeSharePoint GetSharePoint(UserType userType)
        {
            return FindOne(x => x.UserType == userType);
        }

        /// <summary>
        /// 新增用户类型分配比例信息
        /// </summary>
        /// <param name="userType" type="UserType"></param>
        /// <param name="point" type="decimal"></param>
        /// <returns type="UserTypeSharePoint">用户类型分配比例信息</returns>
        public UserTypeSharePoint Add(UserType userType, decimal point)
        {
            UserTypeSharePoint sharePoint = new UserTypeSharePoint(point, userType);
            Add(sharePoint);
            return sharePoint;
        }
    }
}
