﻿using System.Text;
using System.Collections;
using Ydb.Common.Repository;
using Ydb.Finance.DomainModel;
using System;

namespace Ydb.Finance.DomainModel
{
    public interface IRepositorySharePoint : IRepository<UserTypeSharePoint,Guid>
    {

        UserTypeSharePoint GetSharePoint(string userType);
    }
}
