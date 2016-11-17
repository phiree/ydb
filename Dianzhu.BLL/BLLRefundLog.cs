using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Ydb.Common;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 支付项模块 业务业务层
    /// </summary>
    public class BLLRefundLog
    {
        IDAL.IDALRefundLog   dal;
        string errMsg = string.Empty;
        public BLLRefundLog(IDAL.IDALRefundLog   dal)
        {
            this.dal = dal;
        }
        public BLLRefundLog():this(new DAL.DALRefundLog())
        {
        }
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL");
        
        public void Save(RefundLog r)
        {
            dal.Add(r);
        }

        public void Update(RefundLog r)
        {
            dal.Update(r);
        }
    }
}
