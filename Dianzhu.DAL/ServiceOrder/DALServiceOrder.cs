﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
using NHibernate;
using NHibernate.Criterion;
using Dianzhu.IDAL;
using Ydb.Common;

namespace Dianzhu.DAL
{
    public class DALServiceOrder :NHRepositoryBase<ServiceOrder,Guid>, IDALServiceOrder
    {
        public ServiceOrder GetOne(Guid id)
        {
            return FindById(id);
        }
        private IQueryOver<ServiceOrder> GetList(Guid userId, enum_OrderSearchType searchType)
        {
            IQueryOver<ServiceOrder, ServiceOrder> iqueryover = Session.QueryOver<ServiceOrder>().Where(x => x.CustomerId == userId.ToString());

            switch (searchType)
            {

                case enum_OrderSearchType.De:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus == enum_OrderStatus.Finished
                        || x.OrderStatus == enum_OrderStatus.Appraised
                        || x.OrderStatus == enum_OrderStatus.EndWarranty
                        || x.OrderStatus == enum_OrderStatus.EndCancel
                        || x.OrderStatus == enum_OrderStatus.EndRefund
                        || x.OrderStatus == enum_OrderStatus.EndIntervention
                        || x.OrderStatus == enum_OrderStatus.ForceStop
                        );
                    break;
                case enum_OrderSearchType.Nt:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus != enum_OrderStatus.Search
                         && x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Finished
                         && x.OrderStatus != enum_OrderStatus.Appraised
                         && x.OrderStatus != enum_OrderStatus.EndWarranty
                         && x.OrderStatus != enum_OrderStatus.EndCancel
                         && x.OrderStatus != enum_OrderStatus.EndRefund
                         && x.OrderStatus != enum_OrderStatus.EndIntervention
                         && x.OrderStatus != enum_OrderStatus.ForceStop
                    );
                    break;
                default:
                case enum_OrderSearchType.ALL:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Search
                        );
                    break;

            }
            iqueryover = iqueryover.OrderBy(x => x.OrderCreated).Desc;
            return iqueryover;
        }
        public int GetServiceOrderCount(Guid userId, enum_OrderSearchType searchType)
        {
            
                var iqueryover = GetList(userId, searchType);
                int rowCount = iqueryover.RowCount();
                
                return rowCount;
           
        }
        /// <summary>
        /// 除了草稿(draft,draftpushed)之外的订单
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="isCustomerService">是客服的,否则是客户的</param>
        /// <returns></returns>
        public int GetServiceOrderCountWithoutDraft(Guid userid,bool isCustomerService)
        {
            //todo:
            
                var iqueryover = Session.QueryOver<ServiceOrder>();
                iqueryover = isCustomerService ? iqueryover.Where(x => x.CustomerServiceId == userid.ToString())
                                            : iqueryover.Where(x => x.CustomerId == userid.ToString());
                iqueryover = iqueryover.And(x => x.OrderStatus != enum_OrderStatus.Draft && x.OrderStatus != enum_OrderStatus.DraftPushed);
              
                return iqueryover.RowCount();
            
        }
        public decimal GetServiceOrderAmountWithoutDraft(Guid userid, bool isCustomerService)
        {
            //todo:
            
                var iqueryover = Session.QueryOver<ServiceOrder>();
                iqueryover = isCustomerService ? iqueryover.Where(x => x.CustomerServiceId == userid.ToString())
                                            : iqueryover.Where(x => x.CustomerId == userid.ToString());
                iqueryover = iqueryover.And(x => (int)x.OrderStatus != (int)enum_OrderStatus.Draft).And(x => (int)x.OrderStatus != (int)enum_OrderStatus.DraftPushed);

               
                return iqueryover.List().Sum(x => x.DepositAmount);
            
        }

        public IList<ServiceOrder> GetServiceOrderList(Guid userId, enum_OrderSearchType searchType, int pageNum, int pageSize)
        {
            
                var iqueryover = GetList(userId, searchType);
                var result = iqueryover.Skip((pageNum - 1) * pageSize).Take(pageSize).List();
               
                return result;
           
        }

        public IList<ServiceOrder> GetOrderListForBusiness(string businessId, int pageNum, int pageSize, out int totalAmount)
        {
            //var iquery = Session.QueryOver<ServiceOrder>()
            // //   .Where(a => a.Details.Select(x => x.OriginalService).Select(y => y.Business).Contains(business));
            //;
            //.JoinQueryOver(x => x.Service)
            //.JoinQueryOver(y => y.Business)

            
                totalAmount = (int)GetRowCount(x => true);

                IList<ServiceOrder> list = GetAllOrdersForBusiness(new Guid( businessId)).OrderByDescending(x => x.LatestOrderUpdated).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

             
                return list;
            
        }

        public IList<ServiceOrder> GetListForCustomer(string customerId,int pageNum,int pageSize,out int totalAmount)
        {
            
                var iquery = Session.QueryOver<ServiceOrder>().Where(x => x.CustomerId == customerId).Where(x => x.OrderStatus != enum_OrderStatus.Draft).Where(x => x.OrderStatus != enum_OrderStatus.DraftPushed);
                totalAmount = iquery.RowCount();

                IList<ServiceOrder> list = iquery.OrderBy(x => x.OrderFinished).Desc.Skip((pageNum - 1) * pageSize).Take(pageSize).List();

              
               
                return list;
           
           
        }

        /// <summary>
        /// 获得草稿订单
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ServiceOrder GetDraftOrder(string cId, string csId)
        {
            return FindOne(x => x.CustomerId == cId && x.CustomerServiceId == csId && x.OrderStatus == enum_OrderStatus.Draft);
        }

        public IList<ServiceOrder> GetOrderListByDate(string serviceId, DateTime dateTime)
        {
            return Find(x => x.ServiceId == serviceId && x.OrderCreated.Date == dateTime.Date);
        }

        public ServiceOrder GetOrderByIdAndCustomer(Guid Id, string customerId)
        {
            return FindOne(x => x.Id == Id && x.CustomerId == customerId);
        }
        //public IList<ServiceOrder> GetAllOrdersForBusiness(Guid businessId, int pageIndex, int pageSize, out int totalRecords)
        //{
        //    var query = "select o from ServiceOrder as o " +
        //                   " inner join o.Details  as d " +
        //                   "  inner join d.OriginalService as s " +
        //                    " inner join s.Business as b  " +
        //                    " where b.Id='" + businessId + "' " +

        //              " and ( o.OrderStatus!=" + (int)enum_OrderStatus.Draft +
        //                    " or o.OrderStatus!=" + (int)enum_OrderStatus.DraftPushed + " )"
        //                    ;

        //    var list = GetList(query, pageIndex, pageSize, out totalRecords);

        //    return list;
        //}
        public IList<ServiceOrder> GetAllOrdersForBusiness(Guid businessId)
        {
            //todo:
            var query = "select o from ServiceOrder as o "+
                           " inner join o.Details  as d "+
                           "  inner join d.ServiceSnapShot as s " +
                            " where s.BusinessId='" + businessId+"'";
            throw new NotImplementedException();
            // var list=  GetList(query);

            
            //return list;
        }
 
        public IList<ServiceOrder> GetAllCompleteOrdersForBusiness(Guid businessId)
        {
            //todo:
            var query = "select o from ServiceOrder as o " +
                           " inner join o.Details  as d " +
                           "  inner join d.OriginalService as s " +
                            " inner join s.Business as b  " +
                            " where b.Id='" + businessId + "'"+
                            " and ( o.OrderStatus="+(int)enum_OrderStatus.Finished+
                            " or o.OrderStatus="+(int)enum_OrderStatus.Appraised+" )"
                            
                            ;

            throw new NotImplementedException();
            // var list = GetList(query);

           // return list;
        }
        /// <summary>
        /// 用户取消的订单
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public IList<ServiceOrder> GetCustomerCancelForBusiness(Guid businessId)
        {
            //todo:
            var query = "select o from ServiceOrder as o " +
                           " inner join o.Details  as d " +
                           "  inner join d.OriginalService as s " +
                            " inner join s.Business as b  " +
                            " where b.Id='" + businessId + "'" +
                            " and ( o.OrderStatus=" + (int)enum_OrderStatus.Finished +
                            " or o.OrderStatus=" + (int)enum_OrderStatus.Appraised + " )"

                            ;

            throw new NotImplementedException();
            //  var list = GetList(query);

           // return list;
        }
        /// <summary>
        /// 商户取消的订单
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public IList<ServiceOrder> GetBusinessCancelOrdersForBusiness(Guid businessId)
        {
            //todo:
            var query = "select o from ServiceOrder as o " +
                           " inner join o.Details  as d " +
                           "  inner join d.OriginalService as s " +
                            " inner join s.Business as b  " +
                            " where b.Id='" + businessId + "'" +
                            " and ( o.OrderStatus=" + (int)enum_OrderStatus.Finished +
                            " or o.OrderStatus=" + (int)enum_OrderStatus.Appraised + " )"

                            ;

            throw new NotImplementedException();
            // var list = GetList(query);

            //return list;
        }

        public IList<ServiceOrder> GetOrderListOfServiceByDateRange(Guid serviceId, DateTime timeBegin, DateTime timeEnd)
        {
            string query = " select o from ServiceOrder as o "
                           + " inner join o.Details as detail "
                           + " where 1=1"
                           + " and detail.OriginalServiceId='" + serviceId + "' ";
            if (timeBegin != DateTime.MinValue)
            {
                query = query + " and  detail.TargetTime<='" + timeEnd.AddDays(1) + "'";
            }
            if (timeEnd != DateTime.MinValue)
            {
                query = query + " and detail.TargetTime>='" + timeBegin + "'";
            }

            return Session.CreateQuery(query).List<ServiceOrder>();

        }

        public IList<ServiceOrder> GetAll(int pageIndex, int pageSize, out long totalRecords)
        {
            var list=Session.QueryOver<ServiceOrder>()
                 .Fetch(x => x.Details).Eager
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .List();
            totalRecords = Session.QueryOver<ServiceOrder>().RowCount();
            return list;
        }
    }
}
