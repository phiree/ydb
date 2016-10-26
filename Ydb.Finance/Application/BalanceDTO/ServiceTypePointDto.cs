﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Finance.Application
{
    public class ServiceTypePointDto : Entity<Guid>
    {
        /// <summary>
        /// 服务类型ID
        /// </summary>
        public virtual string ServiceTypeId { get; set; }

        /// <summary>
        /// 服务扣点比例
        /// </summary>
        public virtual decimal Point { get; set; }
    }
}