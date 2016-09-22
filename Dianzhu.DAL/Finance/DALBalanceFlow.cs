using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Finance;
using System.Collections;
using NHibernate.Transform;
namespace Dianzhu.DAL.Finance
{
   public  class DALBalanceFlow:NHRepositoryBase<Model.Finance.BalanceFlow,Guid>, IDAL.Finance.IDALBalanceFlow
    {
        
       

        public void Save(Model.Finance.BalanceFlow flow)
        {
            Add(flow);
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
        public IList<Model.Finance.BalanceFlow> GetBillSatistics(string userID,DateTime startTime,DateTime endTime,string serviceTypeLevel,string dateType)
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
            sql += " and t" + serviceTypeLevel;
            sql +=" GROUP BY tt, nn,ii order by tt";
            IList list = Session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToEntityMap).List();
            if (list.Count > 0)
            {
                IList<Model.Finance.BalanceFlow> balanceFlowList = new List<Model.Finance.BalanceFlow>();
                for (int i = 0; i < list.Count; i++)
                {
                    Model.Finance.BalanceFlow balanceFlow = new Model.Finance.BalanceFlow();
                    Hashtable ht = (Hashtable)list[i];
                    balanceFlow.Amount =decimal.Parse(ht["Amount"].ToString());
                    balanceFlow.OccurTime = DateTime.Parse(ht["tt"].ToString());
                    balanceFlow.RelatedObjectId =ht["nn"].ToString();
                    balanceFlow.Id =new Guid(ht["ii"].ToString());
                    balanceFlowList.Add(balanceFlow);
                }
                return balanceFlowList;
            }
            else
            {
                return null;
            }

        }

        ///// <summary>
        ///// 获取流水列表
        ///// </summary>
        ///// <param name="userID"></param>
        ///// <param name="startTime"></param>
        ///// <param name="endTime"></param>
        ///// <param name="serviceTypeLevel"></param>
        ///// <param name="dateType"></param>
        ///// <returns></returns>
        //public IList GetBillList(string userID, DateTime startTime, DateTime endTime, string serviceTypeLevel, string dateType)
        //{
        //    //'%Y-%m-%d'
        //    string sql = @"SELECT b.id,b.amount,b.occurtime,
        //                 t" + serviceTypeLevel + @".Name AS nn,t" + serviceTypeLevel + @".id AS ii 
        //                FROM balanceflow b LEFT JOIN serviceorder o ON b.relatedobjectid = o.id
        //                LEFT JOIN serviceorderdetail d ON o.id = d.serviceorder_id
        //                LEFT JOIN dzservice s ON d.originalservice_id = s.id
        //                LEFT JOIN servicetype t0 ON s.servicetype_id = t0.id
        //                LEFT JOIN servicetype t1 ON t0.parent_id = t1.id
        //                LEFT JOIN servicetype t2 ON t1.parent_id = t2.id where Member_id='" + userID + "'";
        //    if (startTime != DateTime.MinValue)
        //    {
        //        sql += " and DATE_FORMAT(b.occurtime, '" + dateType + "')>=DATE_FORMAT('" + startTime.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + dateType + "')";
        //    }
        //    if (endTime != DateTime.MinValue)
        //    {
        //        sql += " and DATE_FORMAT(b.occurtime, '" + dateType + "')<=DATE_FORMAT('" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + dateType + "')";
        //    }
        //    sql += " and t" + serviceTypeLevel;
        //    sql += " GROUP BY tt, nn,ii order by tt";
        //    IList list = Session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToEntityMap).List();
        //    if (list.Count > 0)
        //    {
        //        IList<Model.Finance.BalanceFlow> balanceFlowList = new List<Model.Finance.BalanceFlow>();
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            Model.Finance.BalanceFlow balanceFlow = new Model.Finance.BalanceFlow();
        //            Hashtable ht = (Hashtable)list[i];
        //            balanceFlow.Amount = decimal.Parse(ht["Amount"].ToString());
        //            balanceFlow.OccurTime = DateTime.Parse(ht["tt"].ToString());
        //            balanceFlow.RelatedObjectId = ht["nn"].ToString();
        //            balanceFlow.Id = new Guid(ht["ii"].ToString());
        //            balanceFlowList.Add(balanceFlow);
        //        }
        //        return balanceFlowList;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}
    }
}
