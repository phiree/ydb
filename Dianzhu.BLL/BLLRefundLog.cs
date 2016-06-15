﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 支付项模块 业务业务层
    /// </summary>
    public class BLLRefundLog
    {
        DAL.DALRefundLog dal;
        string errMsg = string.Empty;
        public BLLRefundLog(DAL.DALRefundLog dal)
        {
            this.dal = dal;
        }
        public BLLRefundLog():this(new DAL.DALRefundLog())
        {
        }
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL");
        
        public void Save(RefundLog r)
        {
            dal.Save(r);
        }

        public void Update(RefundLog r)
        {
            dal.Update(r);
        }
    }
}
