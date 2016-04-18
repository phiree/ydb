using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;

using Dianzhu.DAL;
using Dianzhu.Model.Enums;
using Dianzhu.Pay;

namespace Dianzhu.BLL
{
    public class BLLServiceOrderAppraise
    {
        public DALServiceOrderAppraise dalServiceOrderAppraise = DALFactory.DALServiceOrderAppraise;

        public void Save(ServiceOrderAppraise appraise)
        {
            dalServiceOrderAppraise.Save(appraise);
        }
    }

}
