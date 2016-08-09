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
 
namespace Dianzhu.BLL
{
    /// <summary>
    /// 服务时间段业务逻辑
    /// </summary>
    public class BLLServiceOpenTime
    {
        //20160617_longphui_modify
        //public DALServiceOpenTime DALServiceOpenTime = null;
        IDALServiceOpenTime DALServiceOpenTime = null;
        public BLLServiceOpenTime(IDALServiceOpenTime DALServiceOpenTime)
        {
            //DALServiceOpenTime = DALFactory.DALServiceOpenTime;
            this.DALServiceOpenTime = DALServiceOpenTime;
        }
        public ServiceOpenTime GetOne( Guid id  )
        {
            //return DALServiceOpenTime.GetOne(id);
            return DALServiceOpenTime.FindById(id);
        }
        
        public void Update(ServiceOpenTime sot)
        {
            //DALServiceOpenTime.SaveOrUpdate(sot);
            DALServiceOpenTime.Update(sot);
        }
        
    }
    /// <summary>
    /// 服务时间段业务逻辑
    /// </summary>
    public class BLLServiceOpenTimeForDay
    {
        IDALServiceOpenTimeForDay DALServiceOpenTimeForDay = null;
        public BLLServiceOpenTimeForDay(IDALServiceOpenTimeForDay DALServiceOpenTimeForDay)
        {
            this.DALServiceOpenTimeForDay = DALServiceOpenTimeForDay;
        }
        //public BLLServiceOpenTimeForDay(DALServiceOpenTimeForDay dal)
        //{
        //    DALServiceOpenTimeForDay = dal;
        //}
        public ServiceOpenTimeForDay GetOne(Guid id)
        {
            return DALServiceOpenTimeForDay.FindById(id);
        }
        public void Delete(ServiceOpenTimeForDay sotForDay)
        {
            DALServiceOpenTimeForDay.Delete(sotForDay);
        }
        
        public void Update(ServiceOpenTimeForDay sotForDay)
        {
            //todo: 这里需要判断 该时间段内是否已经有了订单, 如果有了 则不能删除.
            DALServiceOpenTimeForDay.Update(sotForDay);
        }

    }
}
