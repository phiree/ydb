using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDCommon;


namespace Dianzhu.BLL
{
    public class BLLClaimsDetails
    {
        public IDAL.IDALClaimsDetails idalClaimsDetails;

        public BLLClaimsDetails(IDAL.IDALClaimsDetails idalClaimsDetails)
        {
            this.idalClaimsDetails = idalClaimsDetails;
        }

        /// <summary>
        /// 获取理赔状态列表
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="filter"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public IList<Model.ClaimsDetails> GetRefundStatus(Guid orderID,Model.Trait_Filtering filter, Model.Enums.enum_RefundAction action)
        {
            var where = PredicateBuilder.True<Model.ClaimsDetails>();
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.Claims.Order.Id == orderID);
            }
            switch (action)
            {
                case Model.Enums.enum_RefundAction.submit:
                    where = where.And(x => x.Claims.Order.OrderStatus == Model.Enums.enum_OrderStatus.Refund );
                    break;
                case Model.Enums.enum_RefundAction.agree:
                    where = where.And(x => x.Claims.Order.OrderStatus == Model.Enums.enum_OrderStatus.WaitingPayWithRefund);
                    break;
                case Model.Enums.enum_RefundAction.intervention:
                    where = where.And(x => x.Claims.Order.OrderStatus == Model.Enums.enum_OrderStatus.InsertIntervention);
                    break;
                case Model.Enums.enum_RefundAction.cancel:
                    where = where.And(x => x.Claims.Order.OrderStatus == Model.Enums.enum_OrderStatus.EndRefund);
                    break;
                case Model.Enums.enum_RefundAction.refund:
                    where = where.And(x => x.Claims.Order.OrderStatus == Model.Enums.enum_OrderStatus.isRefund);
                    break;
                case Model.Enums.enum_RefundAction.reject:
                    where = where.And(x => x.Claims.Order.OrderStatus == Model.Enums.enum_OrderStatus.RejectRefund);
                    break;
                case Model.Enums.enum_RefundAction.askPay:
                    where = where.And(x => x.Claims.Order.OrderStatus == Model.Enums.enum_OrderStatus.AskPayWithRefund);
                    break;
            }
            Model.ClaimsDetails baseone = null;
            if (filter.baseID != null && filter.baseID != "")
            {
                try
                {
                    baseone = idalClaimsDetails.FindById(new Guid(filter.baseID));
                }
                catch
                {
                    baseone = null;
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? idalClaimsDetails.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : idalClaimsDetails.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;
        }
    }
}
