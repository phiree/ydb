using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.Model
{
    public class ImageInfo : DDDCommon.Domain.Entity<Guid>
    {
        /// <summary>
        /// 上传时间
        /// </summary>
        public virtual DateTime UploadTime { get; set; }

        /// <summary>
        /// 文件名称，用于生成Url
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        /// 高、长
        /// </summary>
        public virtual int Height { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public virtual int Width { get; set; }

        ///// <summary>
        ///// 低分辨率头像的完整路径
        ///// </summary>
        //public virtual string LowUrl
        //{
        //    get
        //    {
        //        return FileName != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + FileName : "";
        //    }
        //}

        ///// <summary>
        ///// 高分辨率的头像完整路径
        ///// </summary>
        //public virtual string HdUrl { get; set; }

        /// <summary>
        /// 文件大小 KB
        /// </summary>
        public virtual int Size { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public virtual DateTime UseTime { get; set; }
    }
}
