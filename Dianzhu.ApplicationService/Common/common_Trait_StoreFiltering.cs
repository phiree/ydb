﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_StoreFiltering
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        /// <type>string</type>
        public string name { get; set; }

        /// <summary>
        /// 归属的店家
        /// </summary>
        /// <type>string</type>
        public string merchantID { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        /// <type>string</type>
        public string staffID { get; set; }
    }
}
