using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.BillSatistic
{
    public class BillSatisticService: IBillSatisticService
    {
        BLL.Finance.IBalanceFlowService iBalanceFlowService;
        public BillSatisticService(BLL.Finance.IBalanceFlowService iBalanceFlowService)
        {
            this.iBalanceFlowService = iBalanceFlowService;
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
        public IList<billStatement> GetBillSatistics( common_Trait_BillFiltering bill, Customer customer)
        {
            IList<Model.Finance.BalanceFlow> billList = null;
            billList = iBalanceFlowService.GetBillSatistics(customer.UserID,utils.CheckDateTime(bill.startTime, "yyyyMMdd", "开始日期startTime"), utils.CheckDateTime(bill.endTime, "yyyyMMdd", "开始日期startTime"),bill.serviceTypeLevel, "%Y-%m-%d");
            return GetBillStatement(billList, "yyyy-MM-dd");
        }

        /// <summary>
        /// 根据月份统计账单结果
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="assign"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<billStatement> GetMonthBillStatement(common_Trait_BillFiltering bill, Customer customer)
        {
            IList<Model.Finance.BalanceFlow> billList = null;
            billList = iBalanceFlowService.GetBillSatistics(customer.UserID, utils.CheckDateTime(bill.startTime, "yyyyMM", "开始月份startTime"), utils.CheckDateTime(bill.endTime, "yyyyMM", "开始月份startTime"), bill.serviceTypeLevel, "%Y-%m");
            return GetBillStatement(billList, "yyyy-MM");
        }

        IList<billStatement> GetBillStatement(IList<Model.Finance.BalanceFlow> billList, string dateType)
        {
            IList<billStatement> billStatementList = new List<billStatement>();
            if (billList == null)
            {
                return billStatementList;
            }
            DateTime tempTime = DateTime.Now;
            decimal sumAmount = 0;
            IList<serviceTypeBillObj> serviceTypeBillObjList = new List<serviceTypeBillObj>();
            billStatement billstatement = new billStatement();
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
                    billstatement = new billStatement();
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
    }
}
