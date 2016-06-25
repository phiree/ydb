using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.Model;
using Dianzhu.DAL;
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
        public IDAL.IDALServiceOpenTime  DALServiceOpenTime = null;
       
        public BLLServiceOpenTime(IDAL.IDALServiceOpenTime dal)
        {
            DALServiceOpenTime = dal;
        }
        public ServiceOpenTime GetOne( Guid id  )
        {
            return DALServiceOpenTime.FindById(id);
        }
        
        public void Update(ServiceOpenTime sot)
        {
            DALServiceOpenTime.Update(sot);
        }
        
    }
    /// <summary>
    /// 服务时间段业务逻辑
    /// </summary>
    public class BLLServiceOpenTimeForDay
    {
         IDAL.IDALServiceOpenTimeForDay DALServiceOpenTimeForDay = null;
        
        public BLLServiceOpenTimeForDay(IDAL.IDALServiceOpenTimeForDay DALServiceOpenTimeForDay)
        {
          this.  DALServiceOpenTimeForDay = DALServiceOpenTimeForDay;
        }
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
