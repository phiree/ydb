using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using NHibernate;
using NHibernate.Criterion;
namespace Dianzhu.DAL
{
    public class DALServiceOrderRemind : DALBase<ServiceOrderRemind>
    {
        public DALServiceOrderRemind()
        {

        }
        //注入依赖,供测试使用;
        public DALServiceOrderRemind(string fortest) : base(fortest)
        {

        }

        public ServiceOrderRemind GetOneByIdAndUserId(Guid Id,Guid UserId)
        {
            return Session.QueryOver<ServiceOrderRemind>().Where(x => x.Id == Id).And(x => x.UserId == UserId).SingleOrDefault();
        }

        public IQueryOver<ServiceOrderRemind> GetList(Guid userId, DateTime startTime, DateTime endTime)
        {
            return Session.QueryOver<ServiceOrderRemind>().Where(x => x.UserId == userId).And(x => x.RemindTime >= startTime).And(x => x.RemindTime <= endTime);
        }

        public int GetSumByUserIdAndDatetime(Guid userId, DateTime startTime, DateTime endTime)
        {
            var query = GetList(userId, startTime, endTime);
            return query.RowCount();
        }

        public IList<ServiceOrderRemind> GetListByUserIdAndDatetime(Guid userId,DateTime startTime,DateTime endTime)
        {
            var query = GetList(userId, startTime, endTime);
            return query.List();
        }
    }
}
