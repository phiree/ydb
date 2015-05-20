using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 行政区划
    /// </summary>
    public class Area
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        /// <summary>
        /// 上级行政单位
        /// </summary>
        public virtual Area Parent { get; set; }
    }
}
