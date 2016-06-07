using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Dianzhu.Model
{
    /// <summary>
    /// 城市
    /// </summary>
    public class City
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 城市名
        /// </summary>
        public virtual string CityName { get; set; }
        /// <summary>
        /// 城市代码
        /// </summary>
        public virtual string CityCode { get; set; }
        /// <summary>
        /// 城市拼音码
        /// </summary>
        public virtual string CityPinyin { get; set; }
    }
}
