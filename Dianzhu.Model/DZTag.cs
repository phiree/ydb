using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 标签类
    /// </summary>
    public class DZTag:DDDCommon.Domain.Entity<Guid>
    {
        /// <summary>
        /// 标签文本
        /// </summary>
        public virtual string Text { get; set; }
        /// <summary>
        /// 原始文本,可能在合并同义标签时被修改为Text
        /// </summary>
        public virtual string OriginalText { get; set; }
        /// <summary>
        /// 标签创建时间
        /// </summary>
        public virtual DateTime CreateDate { get; set; }
        /// <summary>
        /// 所属对象的主键(一般为某个对象的ID).本项目: ServiceID
        /// </summary>
        public virtual string ForPK { get; set; }
        /// <summary>
        /// 第二个关联对象的主键.本项目:ServiceTypeID
        /// </summary>
        public virtual string ForPK2 { get; set; }
        /// <summary>
        /// 第三个关联对象的主键.本项目:BusinessId
        /// </summary>
        public virtual string ForPK3 { get; set; }

    }
}
