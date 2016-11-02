using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Dianzhu.Model.Finance;
using Dianzhu.DAL.Finance;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
namespace Dianzhu.BLL.Finance
{
    
    public class BLLSharePoint:IBLLSharePoint
 
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Bll.Finance.BllSharePoint");
        IDAL.Finance.IDALSharePoint dalSharePoint;
        IDAL.Finance.IDALDefaultSharePoint dalDefaultSharePoint;
          public BLLSharePoint(IDAL.Finance.IDALSharePoint dalSharePoint, IDAL.Finance.IDALDefaultSharePoint dalDefaultSharePoint)
        {
            this.dalSharePoint = dalSharePoint;
            this.dalDefaultSharePoint = dalDefaultSharePoint;
        }
 
        public  void Save(DefaultSharePoint defaultSharePoint)
        {
           var sharePoint= dalDefaultSharePoint.GetDefaultSharePoint(defaultSharePoint.UserType);
            if (sharePoint == null)
            {
                dalDefaultSharePoint.Add(defaultSharePoint);
            }
            else
            {
                sharePoint.Point = defaultSharePoint.Point;
                dalDefaultSharePoint.Update(sharePoint);
            }
        }
 
        public decimal GetSharePoint(MemberDto member)
        {
            var memberPoint = dalSharePoint.GetSharePoint(member.Id.ToString());
            decimal defaultPoint=0;
            if (memberPoint==null)
            {
                DefaultSharePoint defaultSharePoint = dalDefaultSharePoint.GetDefaultSharePoint(member.UserType);
                if (defaultSharePoint != null)
                {
                    defaultPoint = defaultSharePoint.Point;
                }
              
            }
          
            string errMsg = string.Empty;
            if (defaultPoint == 0) {
                errMsg = "该用户及其对应的用户类型未设置分成比例" + member.UserName;
                log.Error(errMsg);
                throw new Exception(errMsg);
            }
            return defaultPoint;
        }
 
        public IList<Dianzhu.Model.Finance.DefaultSharePoint> GetAll()
        {
            return dalDefaultSharePoint.Find(x=>true);
        }
        public DefaultSharePoint GetOne(Guid id)
        {
            return dalDefaultSharePoint.FindById(id);
        }
 
    }
}
