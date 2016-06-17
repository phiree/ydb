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
         public DALDeviceBind()
        {
             
        }
        //注入依赖,供测试使用;
         
        public void UpdateBindStatus(DZMembership member, string appToken, string appName)
        {
            //解除所有 apptoken  和 member的绑定
            string unbind_sql = "update DeviceBind db set db.IsBinding=0 where db.DZMembership.Id='" + member.Id
                                + "' or  db.AppToken='"+ appToken + "'";
            IQuery query = Session.CreateQuery(unbind_sql);
            query.ExecuteUpdate();

            //记录本次绑定
            DeviceBind newDb = new DeviceBind { DZMembership = member, AppName = appName, BindChangedTime = DateTime.Now, AppToken = appToken, IsBinding = true };
            Add(newDb);

        }


        public void SaveOrUpdate(DeviceBind devicebind)
        {
            //解除之前所有 apptoken  和 member的绑定
            string unbind_sql = "update DeviceBind db set db.IsBinding=0,db.BindChangedTime='" + devicebind.BindChangedTime.ToString("yyyy-MM-dd HH:mm:ss")
                                + "' where db.IsBinding=1 and( ";
            if (devicebind.DZMembership == null)
            {
                unbind_sql += "db.DZMembership.Id is null " ;
            }
            else
            {
                unbind_sql += "db.DZMembership.Id='" + devicebind.DZMembership.Id+"'";
            }
            unbind_sql += " or  db.AppToken='" + devicebind.AppToken + "')";
            IQuery query = Session.CreateQuery(unbind_sql);
            query.ExecuteUpdate();

            //记录本次绑定
            Add(devicebind);

        }

        public DeviceBind getDevBindByUUID(Guid uuid)
        {
            return Session.QueryOver<DeviceBind>().Where(x => x.AppUUID == uuid).And(x=>x.IsBinding==true).SingleOrDefault();
        }
        public DeviceBind getDevBindByUserID(DZMembership user)
        {
            return Session.QueryOver<DeviceBind>().Where(x => x.DZMembership == user).And(x => x.IsBinding == true).SingleOrDefault();
        }
    }
}
