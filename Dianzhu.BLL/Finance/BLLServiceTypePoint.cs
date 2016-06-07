﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL.Finance
{
   public  class BLLServiceTypePoint:IBLLServiceTypePoint
    {
        /// <summary>
        /// 服务类别的提成比例
        /// </summary>
        public BLLServiceTypePoint() : this(new DAL.Finance.DALServiceTypePoint()) { }
        public BLLServiceTypePoint(string fortest) { }
        DAL.Finance.DALServiceTypePoint dalServiceTypePoint;
        public BLLServiceTypePoint(DAL.Finance.DALServiceTypePoint dalServiceTypePoint)
        {
            this.dalServiceTypePoint = dalServiceTypePoint;
        }
        public void Add(Model.Finance.ServiceTypePoint serviceTypePoint)
        {
            dalServiceTypePoint.Add(serviceTypePoint);
        }
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
                   
                   throw new Exception("该服务类型的扣点比例未设置:"+serviceType.Name);
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
    }
}
