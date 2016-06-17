﻿using System;
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
            //解除所有 apptoken  和 member的绑定
            string unbind_sql = "update DeviceBind db set db.IsBinding=0 where db.DZMembership.Id='" + member.Id
                                + "' or  db.AppToken='"+ appToken + "'";
            IQuery query = Session.CreateQuery(unbind_sql);
            query.ExecuteUpdate();

            //记录本次绑定
            DeviceBind newDb = new DeviceBind { DZMembership = member, AppName = appName, BindChangedTime = DateTime.Now, AppToken = appToken, IsBinding = true };
            Add(newDb);

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
