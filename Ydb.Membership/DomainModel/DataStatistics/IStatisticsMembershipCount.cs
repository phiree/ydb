﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Membership.DomainModel.DataStatistics
{
    public interface IStatisticsMembershipCount
    {
        long StatisticsLoginCountLastMonth(IList<DZMembership> memberList, IList<MembershipLoginLog> loginList);
        StatisticsInfo StatisticsNewMembershipsCountListByTime(IList<DZMembership> memberList, DateTime beginTime, DateTime endTime, bool IsHour);
        StatisticsInfo StatisticsAllMembershipsCountListByTime(IList<DZMembership> memberList, DateTime beginTime, DateTime endTime, bool IsHour);
        StatisticsInfo StatisticsLoginCountListByTime(IList<DZMembership> memberList, IList<MembershipLoginLog> loginList, DateTime beginTime, DateTime endTime, bool IsHour);
        StatisticsInfo StatisticsAllMembershipsCountListBySex(IList<DZMembership> memberList);
    }
}