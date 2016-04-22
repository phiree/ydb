using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.DomainModel
{
    /// <summary>
    /// 扣点比例
    /// </summary>
   public class SalePoint:Helpers.Domain.IDomainEntity
    {
        public Guid Id { get; private set; }
        /// <summary>
        /// 类型ID
        /// </summary>
        public Guid TypeId { get; private set; }
        /// <summary>
        /// 扣点
        /// </summary>
        public decimal Point { get;private set; }
        /// <summary>
        /// 设置类型的扣点比例
        /// </summary>
        /// <param name="typeid"></param>
        /// <param name="point"></param>
        public void SetPoint(Guid typeid, decimal point)
        {
            this.TypeId = typeid;
            this.Point = point;
        }

    }
}
