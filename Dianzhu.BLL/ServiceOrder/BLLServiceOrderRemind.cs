using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Ydb.Common;
using Dianzhu.Pay;
using DDDCommon;
using Ydb.Common.Specification;

namespace Dianzhu.BLL
{

    public class BLLServiceOrderRemind
    {
        //20150616_longphui_modify
        //public DALServiceOrderRemind dalServiceOrderRemind = DALFactory.DALServiceOrderRemind;
        private IDALServiceOrderRemind dalServiceOrderRemind;
        public BLLServiceOrderRemind(IDALServiceOrderRemind dalServiceOrderRemind)
        {
            this.dalServiceOrderRemind = dalServiceOrderRemind;
        }

        public void Save(ServiceOrderRemind Remind)
        {
            dalServiceOrderRemind.Add(Remind);
        }

        public void Update(ServiceOrderRemind Remind)
        {
            dalServiceOrderRemind.Update(Remind);
        }

        public ServiceOrderRemind GetOneByIdAndUserId(Guid Id, Guid UserId)
        {
            return dalServiceOrderRemind.GetOneByIdAndUserId(Id, UserId);
        }

        public int GetSumByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
        {
            return dalServiceOrderRemind.GetSumByUserIdAndDatetime(userId, startTime, endTime);
        }

        public IList<ServiceOrderRemind> GetListByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
        {
            IList<ServiceOrderRemind> remindList = new List<ServiceOrderRemind>();

            if (startTime < endTime)
            {
                remindList = dalServiceOrderRemind.GetListByUserIdAndDatetime(userId, startTime, endTime);
            }

            return remindList;
        }

        /// <summary>
        /// 条件读取提醒
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderID"></param>
        /// <param name="userId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public IList<Model.ServiceOrderRemind> GetReminds( TraitFilter filter, Guid orderID, Guid userId, DateTime startTime, DateTime endTime)
        {
            var where = PredicateBuilder.True<ServiceOrderRemind>();
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.OrderId == orderID);
            }
            if (userId != Guid.Empty)
            {
                where = where.And(x => x.UserId == userId);
            }
            if (startTime != DateTime.MinValue)
            {
                where = where.And(x => x.RemindTime >= startTime);
            }
            if (endTime != DateTime.MinValue)
            {
                where = where.And(x => x.RemindTime <= endTime);
            }

            ServiceOrderRemind baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = dalServiceOrderRemind.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? dalServiceOrderRemind.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : dalServiceOrderRemind.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;

        }

        /// <summary>
        /// 统计提醒的数量
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="userId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public long GetRemindsCount(Guid orderID, Guid userId, DateTime startTime, DateTime endTime)
        {
            var where = PredicateBuilder.True<ServiceOrderRemind>();
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.OrderId == orderID);
            }
            if (userId != Guid.Empty)
            {
                where = where.And(x => x.UserId == userId);
            }
            if (startTime != DateTime.MinValue)
            {
                where = where.And(x => x.RemindTime >= startTime);
            }
            if (endTime != DateTime.MinValue)
            {
                where = where.And(x => x.RemindTime <= endTime);
            }
            long count = dalServiceOrderRemind.GetRowCount(where);
            return count;
        }

        /// <summary>
        /// 根据ID获取提醒
        /// </summary>
        /// <param name="RemindId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Model.ServiceOrderRemind GetRemindById(Guid RemindId,Guid userId)
        {
            var where = PredicateBuilder.True<ServiceOrderRemind>();
            where = where.And(x => x.Id == RemindId);
            if (userId != Guid.Empty)
            {
                where = where.And(x => x.UserId == userId);
            }
            var remind = dalServiceOrderRemind.FindOne(where);
            return remind;
        }

        /// <summary>
        /// 根据ID删除提醒
        /// </summary>
        /// <param name="remind"></param>
        public void DeleteRemindById(Model.ServiceOrderRemind remind)
        {
            dalServiceOrderRemind.Delete(remind);
        }

    }


}
