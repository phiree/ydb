﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Ydb.Common.Repository;
using Ydb.Finance.DomainModel;
namespace Ydb.Finance.DomainModel
{
    public interface IRepositoryBalanceFlow:IRepository<BalanceFlow,Guid>
    {
        /// <summary>
        /// 统计账单结果
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="serviceTypeLevel"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        IList<BalanceFlow> GetBillSatistics(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, string dateType);
        /// <summary>
        /// 根据用户ID获取用户的账单
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="serviceTypeLevel"></param>
        /// <param name="status"></param>
        /// <param name="billType"></param>
        /// <param name="orderId"></param>
        /// <param name="billServiceType"></param>
        /// <param name="filter"></param>
        /// <param name=""></param>
        /// <returns></returns>
        IList GetBillList(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, string status, string billType, string orderId, string billServiceType,string filter);




    }
}