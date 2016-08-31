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
        public void UpdateBindStatus(DZMembership member, string appToken, string appName)
        {
            using (var t = Session.BeginTransaction())
            {
                //解除所有 apptoken  和 member的绑定
                string unbind_sql = "update DeviceBind db set db.IsBinding=0 where db.DZMembership.Id='" + member.Id
                                + "' or  db.AppToken='" + appToken + "'";
                IQuery query = Session.CreateQuery(unbind_sql);
                query.ExecuteUpdate();

                t.Commit();
            }

            //记录本次绑定
            DeviceBind newDb = new DeviceBind { DZMembership = member, AppName = appName, BindChangedTime = DateTime.Now, AppToken = appToken, IsBinding = true };
            Add(newDb);

        }

        /// <summary>
        /// 解除之前所有 apptoken  和 member的绑定,然后保存新的绑定
        /// </summary>
        /// <param name="devicebind"></param>
        public void UpdateAndSave(DeviceBind devicebind)
        {
            DeviceBind db= FindOne(x => x.AppUUID == devicebind.AppUUID && x.IsBinding == true && x.DZMembership.Id== devicebind.DZMembership.Id);
            if (db == null)
            {
                using (var t = Session.BeginTransaction())
                {
                    //解除之前所有 apptoken  和 member的绑定
                    string unbind_sql = "update DeviceBind db set db.IsBinding=0,db.BindChangedTime='" + devicebind.BindChangedTime.ToString("yyyy-MM-dd HH:mm:ss")
                                    + "' where db.IsBinding=1 and( ";
                    if (devicebind.DZMembership == null)
                    {
                        unbind_sql += "db.DZMembership.Id is null ";
                    }
                    else
                    {
                        unbind_sql += "db.DZMembership.Id='" + devicebind.DZMembership.Id + "'";
                    }
                    unbind_sql += " or  db.AppUUID='" + devicebind.AppUUID + "')";
                    IQuery query = Session.CreateQuery(unbind_sql);
                    query.ExecuteUpdate();

                    t.Commit();
                }

                //记录本次绑定
                Add(devicebind);
            }
            else
            {
                db.IsBinding = true;
                db.BindChangedTime = devicebind.BindChangedTime;
                //blldevicebind.Update(devicebinduuid);
            }

        }

        public DeviceBind getDevBindByUUID(Guid uuid)
        {
            return FindOne(x => x.AppUUID == uuid && x.IsBinding == true);
        }
        public DeviceBind getDevBindByUserID(DZMembership user)
        {
            return FindOne(x => x.DZMembership.Id == user.Id && x.IsBinding == true);
        }
    }
}
