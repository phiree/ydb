using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Ydb.Finance.Application;

namespace Dianzhu.ApplicationService.BillSatistic
{
    public class BillSatisticService: IBillSatisticService
    {
       IBalanceFlowService iBalanceFlowService;
        BLL.BLLServiceType bllServiceType;
        public BillSatisticService(IBalanceFlowService iBalanceFlowService, BLL.BLLServiceType bllServiceType)
        {
            this.iBalanceFlowService = iBalanceFlowService;
            this.bllServiceType = bllServiceType;
        }

        //Dianzhu.IDAL.Finance.IDALBalanceFlow dalBalance = Bootstrap.Container.Resolve<Dianzhu.IDAL.Finance.IDALBalanceFlow>();
        //IList<Dianzhu.Model.Finance.BalanceFlow> balanceList = dalBalance.Find(x => x.Member.Id == CurrentBusiness.Owner.Id);
        //int totalAmount;
        //IList<ServiceOrder> orderList = bllOrder.GetListForBusiness(CurrentBusiness, 0, 99999, out totalAmount);
        //var filteredList = balanceList.Where(x => orderList.Select(y => y.Id.ToString()).ToList().Contains(x.RelatedObjectId));
        //rpFinanceList.DataSource = filteredList;
        //rpFinanceList.DataBind();

        /// <summary>
        /// 根据日期统计账单结果
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="assign"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<billStatementObj> GetBillSatistics( common_Trait_BillFiltering bill, Customer customer)
        {
            IList<BalanceFlowDto> billList = null;
            billList = iBalanceFlowService.GetBillSatistics(customer.UserID,utils.CheckDateTime(bill.startTime, "yyyyMMdd", "开始日期startTime"), utils.CheckDateTime(bill.endTime, "yyyyMMdd", "结束日期startTime"),bill.serviceTypeLevel, "%Y-%m-%d");
            return GetBillStatement(billList, "yyyy-MM-dd");
        }

        /// <summary>
        /// 根据月份统计账单结果
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="assign"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<billStatementObj> GetMonthBillStatement(common_Trait_BillFiltering bill, Customer customer)
        {
            IList<BalanceFlowDto> billList = null;
            billList = iBalanceFlowService.GetBillSatistics(customer.UserID, utils.CheckDateTime(bill.startTime, "yyyyMM", "开始月份startTime"), utils.CheckDateTime(bill.endTime, "yyyyMM", "结束月份startTime"), bill.serviceTypeLevel, "%Y-%m");
            return GetBillStatement(billList, "yyyy-MM");
        }

        /// <summary>
        /// 根据查询结果统计账单结果
        /// </summary>
        /// <param name="billList"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        IList<billStatementObj> GetBillStatement(IList<BalanceFlowDto> billList, string dateType)
        {
            IList<billStatementObj> billStatementList = new List<billStatementObj>();
            if (billList == null)
            {
                return billStatementList;
            }
            DateTime tempTime = DateTime.Now;
            decimal sumAmount = 0;
            IList<serviceTypeBillObj> serviceTypeBillObjList = new List<serviceTypeBillObj>();
            billStatementObj billstatement = new billStatementObj();
            for (int i = 0; i < billList.Count; i++)
            {
                if (tempTime != billList[i].OccurTime)
                {
                    if (i != 0)
                    {
                        billstatement.Income = sumAmount.ToString();
                        billstatement.ServiceTypeBillObj = serviceTypeBillObjList;
                        billStatementList.Add(billstatement);
                    }
                    billstatement = new billStatementObj();
                    serviceTypeBillObjList = new List<serviceTypeBillObj>();
                    billstatement.date = billList[i].OccurTime.ToString(dateType);
                    billstatement.Expenditure = "0";
                    tempTime = billList[i].OccurTime;
                    sumAmount = 0;
                }
                sumAmount = sumAmount + billList[i].Amount;
                serviceTypeBillObj typeBillObj = new serviceTypeBillObj();
                typeBillObj.id = billList[i].Id.ToString();
                typeBillObj.Income = billList[i].Amount.ToString();
                typeBillObj.Expenditure = "0";
                typeBillObj.serviceType = billList[i].RelatedObjectId;
                serviceTypeBillObjList.Add(typeBillObj);
            }
            if (billList.Count > 0)
            {
                billstatement.Income = sumAmount.ToString();
                billstatement.ServiceTypeBillObj = serviceTypeBillObjList;
                billStatementList.Add(billstatement);
            }
            return billStatementList;
        }

        /// <summary>
        /// 根据用户ID获取用户的账单
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="bill"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<billModelObj> GetBillList(common_Trait_Filtering filter, common_Trait_BillModelFiltering bill, Customer customer)
        {
            IList<billModelObj> billModelList = new List<billModelObj>();
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "BillSatisticService");
            if (!string.IsNullOrEmpty(bill.billServiceType))
            {
                Model.ServiceType servicetype = bllServiceType.GetOne(utils.CheckGuidID(bill.billServiceType, "bill.billServiceType"));
                if (servicetype != null)
                {
                    bill.serviceTypeLevel = servicetype.DeepLevel.ToString();
                }
            }
            IList l = iBalanceFlowService.GetBillList(customer.UserID, utils.CheckDateTime(bill.startTime, "yyyyMMdd", "开始日期startTime"), utils.CheckDateTime(bill.endTime, "yyyyMMdd", "结束日期startTime"), bill.serviceTypeLevel,bill.status,bill.billType,bill.orderId,bill.billServiceType,filter1.filter2);
            if (l != null)
            {
                for (int i = 0; i < l.Count; i++)
                {
                    billModelObj billmodel = new billModelObj();
                    Hashtable ht = (Hashtable)l[i];
                    billmodel.id = ht["id"] == null ? "" : ht["id"].ToString();
                    billmodel.createTime = ht["createTime"] == null ? "" : ht["createTime"].ToString();
                    billmodel.serialNo = ht["serialNo"] == null ? "" : ht["serialNo"].ToString();
                    billmodel.type = ht["type"] == null ? "" : ht["type"].ToString();
                    billmodel.amount = ht["amount"] == null ? "" : ht["amount"].ToString();
                    billmodel.discount = ht["discount"] == null ? "" : ht["discount"].ToString();
                    billmodel.billOrderInfo.orderId = ht["orderId"] == null ? "" : ht["orderId"].ToString();
                    billmodel.billOrderInfo.orderAmount = ht["orderAmount"] == null ? "" : ht["orderAmount"].ToString();
                    billmodel.billOrderInfo.customerName = ht["customerName"] == null ? "" : ht["customerName"].ToString();
                    billmodel.billOrderInfo.customerImgUrl = ht["customerImgUrl"] == null ? "" : Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + ht["customerImgUrl"].ToString();
                    billmodel.billOrderInfo.serviceType = ht["serviceType"] == null ? "" : ht["serviceType"].ToString();
                    billModelList.Add(billmodel);
                }
            }
            return billModelList;
        }
    }
}
