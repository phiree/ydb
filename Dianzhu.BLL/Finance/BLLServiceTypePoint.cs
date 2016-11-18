using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Dianzhu.IDAL.Finance;
namespace Dianzhu.BLL.Finance
{/*
    /// <summary>
    /// 服务类型分成比
    /// </summary>
   public  class BLLServiceTypePoint:IBLLServiceTypePoint
    {
        
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BllServiceTypePoint");
        /// <summary>
        /// 服务类别的提成比例
        /// </summary>
       
       IDALServiceTypePoint dalServiceTypePoint;
        public BLLServiceTypePoint( IDALServiceTypePoint dalServiceTypePoint)
        {
            this.dalServiceTypePoint = dalServiceTypePoint;
        }
        public void Add(Model.Finance.ServiceTypePoint serviceTypePoint)
        {
            dalServiceTypePoint.Add(serviceTypePoint);
        }
        string errmsg;
        public decimal GetPoint(Model.ServiceType serviceType)
        {
             var serviceTypePoint= dalServiceTypePoint.GetOneByServiceType(serviceType);
            if (serviceTypePoint == null)
            {
                if (serviceType.Parent != null)
                {
                    return GetPoint(serviceType.Parent);
                }
                else
                {
                   errmsg = "该服务类型的扣点比例未设置:" + serviceType.Name+"("+serviceType.Id+")";
                   log.Error(errmsg);
                   throw new Exception("该服务类型的扣点比例未设置:" + serviceType.Name);
                   
                }
            }
            else
            {
                return serviceTypePoint.Point;
            }
        }
      public  IList<Dianzhu.Model.Finance.ServiceTypePoint> GetAll()
        {
            return dalServiceTypePoint.GetAll ();
        }
        
}*/
}
