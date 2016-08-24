using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Dianzhu.DAL.Client
{
    public class DALUserToken : Dianzhu.DAL.NHRepositoryBase<Model.UserToken, Guid>, IDAL.IDALUserToken
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Model.UserToken GetToken(string userID)
        {
            Model.UserToken usertokenOld = FindOne(x => x.UserID == userID && x.Flag == 1);
            return usertokenOld;
        }

    }
}
