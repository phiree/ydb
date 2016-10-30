﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
 
using Ydb.Membership.DomainModel.Repository;
using NHibernate;
using System.Linq.Expressions;

namespace Ydb.Membership.Infrastructure.Repository.NHibernate
{
   public class RepositoryUserToken:NHRepositoryBase<UserToken,Guid>,IRepositoryUserToken
    {
        

        public  UserToken GetToken(string userID)
        {
            UserToken usertokenOld = FindOne(x => x.UserID == userID && x.Flag == 1);
            return usertokenOld;
        }

    }
}
