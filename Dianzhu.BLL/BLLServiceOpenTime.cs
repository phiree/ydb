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
        public DALServiceOpenTime DALServiceOpenTime = null;
        public BLLServiceOpenTime() { DALServiceOpenTime = DALFactory.DALServiceOpenTime; }
        public BLLServiceOpenTime(DALServiceOpenTime dal)
        {
            DALServiceOpenTime = dal;
        }
        public ServiceOpenTime GetOne( Guid id  )
        {
            return DALServiceOpenTime.GetOne(id);
        }
        public void SaveOrUpdate(ServiceOpenTime sot)
        {
            DALServiceOpenTime.SaveOrUpdate(sot);
        }
        
    }
    /// <summary>
    /// 服务时间段业务逻辑
    /// </summary>
    public class BLLServiceOpenTimeForDay
    {
        public DALServiceOpenTimeForDay DALServiceOpenTimeForDay = null;
        public BLLServiceOpenTimeForDay() { DALServiceOpenTimeForDay = DALFactory.DALServiceOpenTimeForDay; }
        public BLLServiceOpenTimeForDay(DALServiceOpenTimeForDay dal)
        {
            DALServiceOpenTimeForDay = dal;
        }
        public ServiceOpenTimeForDay GetOne(Guid id)
        {
            return DALServiceOpenTimeForDay.GetOne(id);
        }
        
        public void Update(Guid id, int maxNum, bool isEnabled)
        {
            ServiceOpenTimeForDay item = GetOne(id);
            item.Enabled = isEnabled;
            item.MaxOrderForOpenTime = maxNum;
            //todo: 这里需要判断 该时间段内是否已经有了订单, 如果有了 则不能删除.
            DALServiceOpenTimeForDay.Update(item);
        }

    }
}
