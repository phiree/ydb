using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    #region VCM001001
    public class ReqDataVCM001001
    {
        public string uid { get; set; }
        public string userPWord { get; set; }
        public string vcsTarget { get; set; }

    }
    public class RespDataVCM001001
    {
        public string sum { get; set; }
    }
    #endregion

    #region VCM001002
    public class ReqDataVCM001002
    {
        public string uid { get; set; }
        public string userPWord { get; set; }
        public string vcsTarget { get; set; }
        public string pageSize { get; set; }
        public string pageNum { get; set; }

    }
    public class RespDataVCM001002
    {
        public IList<RespDataVCM001002_Cashticket> arrayData { get; set; }
        public RespDataVCM001002()
        {

            arrayData = new List<RespDataVCM001002_Cashticket>();
        }

        public void AdapList(IList<CashTicket> serviceOrderList)
        {
            foreach (CashTicket order in serviceOrderList)
            {
                RespDataVCM001002_Cashticket adapted_order = new RespDataVCM001002_Cashticket().Adap(order);
                arrayData.Add(adapted_order);
            }

        }
    }
    public class RespDataVCM001002_Cashticket
    {
        public string vcsID { get; set; }
        public string srvBiz { get; set; }
        public string srvBizID { get; set; }
        public string vcsStartTime { get; set; }
        public string vcsEndTime { get; set; }
        public string vcsMoney { get; set; }
        public string vcsStatus { get; set; }
        public string vcsExdes { get; set; }

        public RespDataVCM001002_Cashticket Adap(CashTicket cashTicket)
        {
            this.vcsID = cashTicket.TicketCode;
            this.srvBiz = cashTicket.CashTicketTemplate.Business.Name;
            this.srvBizID = cashTicket.CashTicketTemplate.Business.Id.ToString();
            this.vcsStartTime = cashTicket.CashTicketTemplate.ValidDate.ToString("yyyyMMddHHmm");// ServiceTimeBegin;
            this.vcsEndTime = cashTicket.CashTicketTemplate.ExpiredDate.ToString("yyyyMMddHHmm");
            ///这个是服务单价
            this.vcsMoney = cashTicket.CashTicketTemplate.Amount.ToString("0.0");
            this.vcsStatus = string.Empty;
            this.vcsExdes = cashTicket.CashTicketTemplate.Conditions;
            return this;
        }
    }
    #endregion

    #region VCM001003
    public class ReqDataVCM001003
    {

        public string vcsID { get; set; }

    }
    public class RespDataVCM001003
    {
        public RespDataVCM001002_Cashticket vcsObj { get; set; }
    }
    #endregion
}
