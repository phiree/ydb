﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;
namespace Ydb.BusinessResource.DomainModel
{
    public interface IRepositoryBusinessImage:IRepository<BusinessImage,Guid>
    {
        BusinessImage FindBusImageByName(string imgName);

    }
}
