﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Ydb.Common;
using NHibernate;
using NHibernate.Criterion;
namespace Dianzhu.DAL
{
    public class DALServiceOrderAppraise : NHRepositoryBase<ServiceOrderAppraise,Guid>,IDAL.IDALServiceOrderAppraise
    {
        
    }
}
