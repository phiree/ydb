using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.DAL;
using Dianzhu.IDAL;
using Dianzhu.BLL.Validator;
using FluentValidation;
using FluentValidation.Results;
using System.Collections;
using DDDCommon;

namespace Dianzhu.BLL
{
    /// <summary>
    /// 服务货架化: 库存管理
    /// </summary>
    public class BLLDZServiceStock
    {
        IDAL.IDALDZService DALDZService;
        IDAL.IDALServiceOrder dalOrder;
        IDAL.IDALServiceOpenTime dalOpenTime;
        
     
        public BLLDZServiceStock(IDAL.IDALDZService dal,IDAL.IDALServiceOrder dalOrder)
        {
            DALDZService = dal;
            this.dalOrder = dalOrder;
             
        }
        /// <summary>
        /// 获取某个时刻 某个服务的 
        /// 当天最大接单量/已接单量
        /// 所在时段最大接单量/已接单量
        /// </summary>
        /// <param name="service"></param>
        /// <param name="time"></param>
        public void GetStock(DZService service, DateTime time)
        {
            DateTime date = time.Date;
           ServiceOpenTime openTime= service.OpenTimes.Single(x => x.DayOfWeek == date.DayOfWeek);
            int maxForDay = openTime.MaxOrderForDay;
            ServiceOpenTimeForDay forDay = openTime.OpenTimeForDay.SingleOrDefault(x => x.IsIn(time));
            int maxForTime = forDay.MaxOrderForOpenTime;

            //包含当前服务的订单
            int orderCoundForDay=(int) dalOrder.GetRowCount(x => x.Details.Count(y => y.OriginalService.Id == service.Id) > 0 && x.OrderConfirmTime.Date == date);
             
        }



       
    }
}
