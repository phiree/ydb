using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class snapshortsObj
    {
        string _date = "";
        /// <summary>
        /// 快照的日期（yyyyMMdd）
        /// </summary>
        /// <type>string</type>
        public string date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        IList<orderSnapshot> _ordersnapshot = new List<orderSnapshot>();
        /// <summary>
        /// 订单信息
        /// </summary>
        public IList<orderSnapshot> ordersnapshot
        {
            get
            {
                return _ordersnapshot;
            }
            set
            {
                _ordersnapshot = value;
            }
        }


        public IList<snapshortsObj> Adap(IList<Dianzhu.Model.ServiceOrder> orderList, BLL.BLLServiceOrderStateChangeHis bllstatehis,Order.IOrderService orderService)
        {
            IList<snapshortsObj> snapshortsobjs = new List<snapshortsObj>();
            IList <DateTime> dates = orderList.Select(x => x.OrderCreated.Date).Distinct().ToList();
            foreach (DateTime date in dates)
            {
                IList<Dianzhu.Model.ServiceOrder> orderInDateList = orderList.Where(x => x.OrderCreated.Date == date).ToList();
                //接单量平均值
                snapshortsObj snapshortsobj = new snapshortsObj();
                snapshortsobj.date = date.ToString("yyyyMMdd");
                snapshortsobj.ordersnapshot = new orderSnapshot().Adap(date, orderInDateList, bllstatehis,orderService);
                snapshortsobjs.Add(snapshortsobj);
            }
            return snapshortsobjs;
        }

        //string _maxCountOrder = "";
        ///// <summary>
        ///// 最大订单数
        ///// </summary>
        ///// <type>string</type>
        //public string maxCountOrder
        //{
        //    get
        //    {
        //        return _maxCountOrder;
        //    }
        //    set
        //    {
        //        _maxCountOrder = value;
        //    }
        //}

        //string _havaCountOrder = "";
        ///// <summary>
        ///// 已生成服务的订单数量
        ///// </summary>
        ///// <type>string</type>
        //public string havaCountOrder
        //{
        //    get
        //    {
        //        return _havaCountOrder;
        //    }
        //    set
        //    {
        //        _havaCountOrder = value;
        //    }
        //}

        //IList<workTimeObj> _workTimeArray = new List<workTimeObj>();
        ///// <summary>
        ///// 该快照下包含的工作时间数组
        ///// </summary>
        ///// <type>array[workTimeObj]</type>
        //public IList<workTimeObj> workTimeArray
        //{
        //    get
        //    {
        //        return _workTimeArray;
        //    }
        //    set
        //    {
        //        _workTimeArray = value;
        //    }
        //}
    }
}
