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
            where = where.And(x => x.UserID == usertoken.UserID && x.Flag==1);
            UserToken usertokenOld = dalusertoken.FindOne(where);
            if (usertokenOld != null)
            {
                usertokenOld.Flag = 0;
                dalusertoken.Update(usertokenOld);
            }
            dalusertoken.Add(usertoken);
            usertoken = dalusertoken.FindById(usertoken.Id);
            if (usertoken == null)
            {
                return true;
            }
            return false;
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
