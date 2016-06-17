using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.IDAL;
using Dianzhu.DAL;
using Dianzhu.Model.Enums;
using Dianzhu.Pay;
using DDDCommon;

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

        public void SaveOrUpdate(ServiceOrderRemind Remind)
        {
            //dalServiceOrderRemind.SaveOrUpdate(Remind);
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
        public IList<Model.ServiceOrderRemind> GetReminds(int pagesize, int pagenum, Guid orderID, Guid userId, DateTime startTime, DateTime endTime)
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
            long t = 0;
            var list = pagesize == 0 ? dalServiceOrderRemind.Find(where).ToList() : dalServiceOrderRemind.Find(where, pagenum, pagesize, out t).ToList();
            return list;

        }

        /// <summary>
        /// 统计提醒的数量
        /// </summary>
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
        public Model.ServiceOrderRemind GetRemindById(Guid RemindId)
        {
            var remind = dalServiceOrderRemind.FindById(RemindId);
            return remind;
        }

        /// <summary>
        /// 根据ID删除提醒
        /// </summary>
        public void DeleteRemindById(Model.ServiceOrderRemind remind)
        {
            dalServiceOrderRemind.Delete(remind);
        }

    }


}
