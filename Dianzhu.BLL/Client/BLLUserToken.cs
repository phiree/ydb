using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.IDAL;
using Dianzhu.Model;
using DDDCommon;

namespace Dianzhu.BLL.Client
{
    public class BLLUserToken
    {
        IDALUserToken dalusertoken;
        public BLLUserToken(IDALUserToken dalusertoken)
        {
            this.dalusertoken = dalusertoken;
        }

        /// <summary>
        /// 修改token
        /// </summary>
        /// <param name="usertoken"></param>
        /// <returns></returns>
        public bool addToken(UserToken usertoken)
        {
            var where = PredicateBuilder.True<UserToken>();
            where = where.And(x => x.UserID == usertoken.UserID && x.Flag == 1);
            UserToken usertokenOld = dalusertoken.FindOne(where);
            if (usertokenOld != null)
            {
                //usertokenOld.Flag = 0;
                usertokenOld.Token = usertoken.Token;
                usertokenOld.CreatedTime = usertoken.CreatedTime;
                dalusertoken.Update(usertokenOld);
            }
            else
            {

                //log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.Web.RestfulApi.ClientController");
                //ilog.Debug("PostToken(Baegin3):" + usertoken.Id + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                dalusertoken.Add(usertoken);
                //ilog.Debug("PostToken(Baegin4):" + usertoken.Id + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                //usertoken = dalusertoken.FindById(usertoken.Id);
                //ilog.Debug("PostToken(Baegin5):" + usertoken.Id + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            }
            //if (usertoken == null)
            //{
            return true;
            //}
            //return false;
        }

        public bool CheckToken(string token)
        { 
            var where = PredicateBuilder.True<UserToken>();
            where = where.And(x => x.Token == token && x.Flag == 1);
            UserToken usertokenOld = dalusertoken.FindOne(where);
            if (usertokenOld == null)
            {
                return true;
            }
            return false;
        }

        public UserToken GetToken(string userID)
        {
            return dalusertoken.GetToken(userID);
        }
    }
}
