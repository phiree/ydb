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

namespace Ydb.Finance.Infrastructure.Repository
{
    public class RepositoryUserTypeSharePoint : NHRepositoryBase<UserTypeSharePoint, Guid>, IRepositoryUserTypeSharePoint
    {
        public RepositoryUserTypeSharePoint(ISession session) : base(session)
        {
        }

        /// <summary>
        /// 统计账单结果
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="serviceTypeLevel"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        public IList<BalanceFlow> GetBillSatistics(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, string dateType)

        {
            //'%Y-%m-%d'
            string sql = @"SELECT SUM(b.amount) AS amount, DATE_FORMAT(b.occurtime, '" + dateType + @"') AS tt,
                         t" + serviceTypeLevel + @".Name AS nn,t" + serviceTypeLevel + @".id AS ii 
                        FROM balanceflow b LEFT JOIN serviceorder o ON b.relatedobjectid = o.id
                        LEFT JOIN serviceorderdetail d ON o.id = d.serviceorder_id
                        LEFT JOIN dzservice s ON d.originalservice_id = s.id
                        LEFT JOIN servicetype t0 ON s.servicetype_id = t0.id
                        LEFT JOIN servicetype t1 ON t0.parent_id = t1.id
                        LEFT JOIN servicetype t2 ON t1.parent_id = t2.id where Member_id='" + userID + "'";
            if (startTime != DateTime.MinValue)
            {
                sql += " and DATE_FORMAT(b.occurtime, '" + dateType + "')>=DATE_FORMAT('" + startTime.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + dateType + "')";
            }
            if (endTime != DateTime.MinValue)
            {
                sql += " and DATE_FORMAT(b.occurtime, '" + dateType + "')<=DATE_FORMAT('" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + dateType + "')";
            }
            sql += " and t" + serviceTypeLevel + ".DeepLevel=" + serviceTypeLevel;
            sql += " GROUP BY tt, nn,ii order by tt";
            IList list = session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToEntityMap).List();
            if (list.Count > 0)
            {
                IList<BalanceFlow> balanceFlowList = new List<BalanceFlow>();
                for (int i = 0; i < list.Count; i++)
                {
                    BalanceFlow balanceFlow = new BalanceFlow();
                    Hashtable ht = (Hashtable)list[i];
                    balanceFlow.Amount = ht["amount"] == null ? 0 : decimal.Parse(ht["amount"].ToString());
                    balanceFlow.OccurTime = ht["tt"] == null ? DateTime.MinValue : DateTime.Parse(ht["tt"].ToString());
                    balanceFlow.RelatedObjectId = ht["nn"] == null ? "" : ht["nn"].ToString();
                    balanceFlow.Id = ht["ii"] == null ? Guid.Empty : new Guid(ht["ii"].ToString());
                    balanceFlowList.Add(balanceFlow);
                }
                return balanceFlowList;
            }
            else
            {
                return null;
            }

        }

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
        public IList GetBillList(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, string status, string billType, string orderId, string billServiceType, string filter)
        {
            //'%Y-%m-%d'
            string sql = @"SELECT b.id,b.amount,b.occurtime as createTime,'' as serialNo,b.FlowType as type,'' as discount,o.id as orderId,o.NegotiateAmount as orderAmount,
                         t0.Name AS serviceType,t0.id AS ii ,
                        m.UserName as customerName,m.AvatarUrl as customerImgUrl
                        FROM balanceflow b LEFT JOIN serviceorder o ON b.relatedobjectid = o.id
                        LEFT JOIN dzmembership m ON o.customer_id = m.id
                        LEFT JOIN serviceorderdetail d ON o.id = d.serviceorder_id
                        LEFT JOIN dzservice s ON d.originalservice_id = s.id
                        LEFT JOIN servicetype t0 ON s.servicetype_id = t0.id
                        LEFT JOIN servicetype t1 ON t0.parent_id = t1.id
                        LEFT JOIN servicetype t2 ON t1.parent_id = t2.id where Member_id='" + userID + "'";
            if (startTime != DateTime.MinValue)
            {
                sql += " and DATE_FORMAT(b.occurtime, '%Y-%m-%d')>=DATE_FORMAT('" + startTime.ToString("yyyy-MM-dd HH:mm:ss") + "', '%Y-%m-%d')";
            }
            if (endTime != DateTime.MinValue)
            {
                sql += " and DATE_FORMAT(b.occurtime, '%Y-%m-%d')<=DATE_FORMAT('" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "', '%Y-%m-%d')";
            }
            if (!string.IsNullOrEmpty(billServiceType))
            {
                sql += " and t" + serviceTypeLevel + ".id='" + billServiceType + "'";
                sql += " and t" + serviceTypeLevel + ".DeepLevel=" + serviceTypeLevel;
            }
            //string status,string billType,string orderId
            if (!string.IsNullOrEmpty(status))
            {
            }
            if (!string.IsNullOrEmpty(billType))
            {
                sql += " and b.FlowType='" + billType + "'";
            }
            if (!string.IsNullOrEmpty(orderId))
            {
                sql += " and o.id='" + orderId + "'";
            }
            //filter
            //if (!string.IsNullOrEmpty(filter.sortby))
            //{
            //    sql += "  order by " + filter.sortby;
            //    if (!filter.ascending)
            //    {
            //        sql += " DESC ";
            //    }
            //}
            //if (filter.pageNum != 0)
            //{
            //    sql += " limit " + ((filter.pageNum - 1) * filter.pageSize).ToString() + "," + (filter.pageNum * filter.pageSize - 1).ToString();
            //}
            //limit 0, 999
            IList list = session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToEntityMap).List();
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                return null;
            }

        }

        public UserTypeSharePoint GetSharePoint(UserType userType )
        {
            
                return FindOne(x => x.UserType == userType);
        }

        public UserTypeSharePoint Add(UserType userType, decimal point)
        {
            UserTypeSharePoint sharePoint = new UserTypeSharePoint(point, userType);
            Add(sharePoint);
            return sharePoint;
        }
    }
}
