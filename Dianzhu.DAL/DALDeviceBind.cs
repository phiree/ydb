using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALDeviceBind :NHRepositoryBase<DeviceBind,Guid>,IDAL.IDALDeviceBind
    {

        log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.Web.RestfulApi.DeviceBind");
        public void UpdateBindStatus(string memberId, string appToken, string appName)
        {
            using (var t = Session.BeginTransaction())
            {
                //解除所有 apptoken  和 member的绑定
                string unbind_sql = "update DeviceBind db set db.IsBinding=0 where db.DZMembership.Id='" + memberId
                                + "' or  db.AppToken='" + appToken + "'";
                IQuery query = Session.CreateQuery(unbind_sql);
                query.ExecuteUpdate();

                t.Commit();
            }

            //记录本次绑定
            DeviceBind newDb = new DeviceBind { DZMembershipId = memberId, AppName = appName, BindChangedTime = DateTime.Now, AppToken = appToken, IsBinding = true };
            Add(newDb);

        }

        /// <summary>
        /// 解除之前所有 apptoken  和 member的绑定,然后保存新的绑定
        /// </summary>
        /// <param name="devicebind"></param>
        public void UpdateAndSave(DeviceBind devicebind)
        {
            //DeviceBind db= FindOne(x => x.AppUUID == devicebind.AppUUID && x.IsBinding == true && x.DZMembership.Id== devicebind.DZMembership.Id);
            //if (db == null)
            //{
            //    using (var t = Session.BeginTransaction())
            //    {
            //        //解除之前所有 apptoken  和 member的绑定
            //        string unbind_sql = "update DeviceBind db set db.IsBinding=0,db.BindChangedTime='" + devicebind.BindChangedTime.ToString("yyyy-MM-dd HH:mm:ss")
            //                        + "' where db.IsBinding=1 and( ";
            //        if (devicebind.DZMembership == null)
            //        {
            //            unbind_sql += "db.DZMembership.Id is null ";
            //        }
            //        else
            //        {
            //            unbind_sql += "db.DZMembership.Id='" + devicebind.DZMembership.Id + "'";
            //        }
            //        unbind_sql += " or  db.AppToken='" + devicebind.AppToken + "')";
            //        IQuery query = Session.CreateQuery(unbind_sql);
            //        query.ExecuteUpdate();

            //        t.Commit();
            //    }

            //    //记录本次绑定
            //    Add(devicebind);
            //}
            //else
            //{
            //    db.IsBinding = true;
            //    db.BindChangedTime = devicebind.BindChangedTime;
            //    //blldevicebind.Update(devicebinduuid);
            //}
            DeviceBind db = FindOne(x => x.AppUUID == devicebind.AppUUID);
            //using (var t = Session.BeginTransaction())
            //{
            //    //解除之前所有 apptoken  和 member的绑定
            //    string unbind_sql = "update DeviceBind db set db.IsBinding=0,db.BindChangedTime='" + devicebind.BindChangedTime.ToString("yyyy-MM-dd HH:mm:ss")
            //                    + "' where db.IsBinding=1 and( ";
            //    if (devicebind.DZMembership == null)
            //    {
            //        //unbind_sql += "db.DZMembership.Id is null ";
            //    }
            //    else
            //    {
            //        unbind_sql += "db.DZMembership.Id='" + devicebind.DZMembership.Id + "' or ";
            //    }
            //    unbind_sql += "  db.AppUUID='" + devicebind.AppUUID + "')";
            //    IQuery query = Session.CreateQuery(unbind_sql);
            //    query.ExecuteUpdate();
            //    t.Commit();
            //}
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            if (db == null)
            {
                Add(devicebind);
            }
            else
            {
                ilog.Debug("DeviceBind(1)");
                db.AppToken = devicebind.AppToken;
                db.IsBinding = true;
                db.DZMembershipId = devicebind.DZMembershipId;
                db.BindChangedTime = devicebind.BindChangedTime;
                Update(db);
                ilog.Debug("DeviceBind(2):"+db.Id);
            }
            if (devicebind.DZMembershipId != null)
            {
                IList<DeviceBind> dbs = Find(x => x.AppUUID != devicebind.AppUUID && x.DZMembershipId == devicebind.DZMembershipId);
                foreach (DeviceBind d in dbs)
                {
                    d.IsBinding = false;
                    d.BindChangedTime = devicebind.BindChangedTime;
                    Update(d);
                }
            }
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            

        }

        public DeviceBind getDevBindByUUID(Guid uuid)
        {
            return FindOne(x => x.AppUUID == uuid && x.IsBinding == true);
        }
        public DeviceBind getDevBindByUserID(Guid userId)
        {
            return FindOne(x => x.DZMembershipId == userId.ToString() && x.IsBinding == true);
        }
    }
}
