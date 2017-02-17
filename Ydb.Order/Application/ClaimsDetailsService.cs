using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;
using Ydb.Common.Specification;
using Ydb.Common;
 

namespace Ydb.Order.Application
{
    public class  ClaimsDetailsService : IClaimsDetailsService
    {
        public IRepositoryClaimsDetails repoClaimDetail;

        public ClaimsDetailsService(IRepositoryClaimsDetails repoClaimDetail)
        {
            this.repoClaimDetail = repoClaimDetail;
        }

        /// <summary>
        /// 获取理赔状态列表
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="filter"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public IList<ClaimsDetails> GetRefundStatus(Guid orderID, TraitFilter filter, enum_RefundAction action)
        {
            var where = PredicateBuilder.True<ClaimsDetails>();
            if (orderID != Guid.Empty)
            {
                where = where.And(x => x.Claims.Order.Id == orderID);
            }
            switch (action)
            {
                case enum_RefundAction.submit:
                    where = where.And(x => x.Claims.Order.OrderStatus == enum_OrderStatus.Refund );
                    break;
                case enum_RefundAction.agree:
                    where = where.And(x => x.Claims.Order.OrderStatus == enum_OrderStatus.WaitingPayWithRefund);
                    break;
                case enum_RefundAction.intervention:
                    where = where.And(x => x.Claims.Order.OrderStatus == enum_OrderStatus.InsertIntervention);
                    break;
                case enum_RefundAction.cancel:
                    where = where.And(x => x.Claims.Order.OrderStatus == enum_OrderStatus.EndRefund);
                    break;
                case enum_RefundAction.refund:
                    where = where.And(x => x.Claims.Order.OrderStatus == enum_OrderStatus.isRefund);
                    break;
                case enum_RefundAction.reject:
                    where = where.And(x => x.Claims.Order.OrderStatus == enum_OrderStatus.RejectRefund);
                    break;
                case enum_RefundAction.askPay:
                    where = where.And(x => x.Claims.Order.OrderStatus == enum_OrderStatus.AskPayWithRefund);
                    break;
            }
            ClaimsDetails baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = repoClaimDetail.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? repoClaimDetail.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList()
                : repoClaimDetail.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;
        }
    }
}
