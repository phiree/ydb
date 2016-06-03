using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Dianzhu.Model.Finance;
using Dianzhu.DAL.Finance;
namespace Dianzhu.BLL.Finance
{
    
    public class BLLSharePoint:IBLLSharePoint
 
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Bll.Finance.BllSharePoint");
        DAL.Finance.DALSharePoint dalSharePoint;
        DAL.Finance.DALDefaultSharePoint dalDefaultSharePoint;
        public BLLSharePoint() : this(new DAL.Finance.DALSharePoint(), new DAL.Finance.DALDefaultSharePoint()) { }
        public BLLSharePoint(DAL.Finance.DALSharePoint dalSharePoint, DAL.Finance.DALDefaultSharePoint dalDefaultSharePoint)
        {
            this.dalSharePoint = dalSharePoint;
            this.dalDefaultSharePoint = dalDefaultSharePoint;
        }
 
        public  void Save(DefaultSharePoint defaultSharePoint)
        {
           var sharePoint= dalDefaultSharePoint.GetDefaultSharePoint(defaultSharePoint.UserType);
            if (sharePoint == null)
            {
                dalDefaultSharePoint.Save(defaultSharePoint);
            }
            else
            {
                sharePoint.Point = defaultSharePoint.Point;
                dalDefaultSharePoint.Update(sharePoint);
            }
        }
 
        public decimal GetSharePoint(Model.DZMembership member)
        {
            var memberPoint = dalSharePoint.GetSharePoint(member);
            decimal defaultPoint=0;
            if (memberPoint==null)
            { 
              defaultPoint = dalDefaultSharePoint.GetDefaultSharePoint(member.UserType).Point;
            }
          
            string errMsg = string.Empty;
            if (defaultPoint == 0) {
                errMsg = "该用户及其对应的用户类型未设置分成比例" + member.DisplayName;
                log.Error(errMsg);
                throw new Exception(errMsg);
            }
            return defaultPoint;
        }
 
        public IList<Dianzhu.Model.Finance.DefaultSharePoint> GetAll()
        {
            return dalDefaultSharePoint.GetAll<Dianzhu.Model.Finance.DefaultSharePoint>();
        }
        public DefaultSharePoint GetOne(Guid id)
        {
            return dalDefaultSharePoint.GetOne(id);
        }
 
    }
}
