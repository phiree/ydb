using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALDeviceBind : DALBase<DeviceBind>
    {


        public void UpdateBindStatus(DZMembership member, string appToken, string appName)
        {
            //解除所有 apptoken  和 member的绑定
            string unbind_all_member = "update DeviceBind db set db.BindStatus=0 where db.DZMembership.Id='" + member.Id + "'";// + " and db.AppToken='" + appToken + "'";
            string unbind_all_device = "update DeviceBind db set db.BindStatus=0 where db.AppToken'" + appToken + "'";// + " and db.AppToken='" + appToken + "'";
            IMultiQuery multiQuery = Session.CreateMultiQuery();
            multiQuery.Add(unbind_all_device);
            multiQuery.Add(unbind_all_member);
            multiQuery.List();
            //记录本次绑定
            DeviceBind newDb = new DeviceBind { DZMembership = member, AppName = appName, BindChangedTime = DateTime.Now, AppToken = appToken, IsBinding = true };
            Session.Save(newDb);

        }
    }
}
