using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 退款模块 业务业务层
    /// </summary>
    public class BLLRefund
    {
        DAL.DALRefund dal;
        string errMsg = string.Empty;
        public BLLRefund(DAL.DALRefund dal)
        {
            this.dal = dal;
        }
        public BLLRefund():this(new DAL.DALRefund())
        {
        }
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL");
        
        public void Save(Refund r)
        {
            dal.Save(r);
        }

        public void Update(Refund r)
        {
            r.LastUpdateTime = DateTime.Now;
            dal.Update(r);
        }
    }
}
