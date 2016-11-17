using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;

using Dianzhu.DAL;
using Ydb.Common;
using Dianzhu.Pay;

namespace Dianzhu.BLL
{
    public class BLLServiceOrderAppraise
    {
        IDAL.IDALServiceOrderAppraise dalServiceOrderAppraise;

        public BLLServiceOrderAppraise(IDAL.IDALServiceOrderAppraise dal)
        {
            dalServiceOrderAppraise = dal;
        }

        public void Save(ServiceOrderAppraise appraise)
        {
            dalServiceOrderAppraise.Add(appraise);
        }
    }

}
