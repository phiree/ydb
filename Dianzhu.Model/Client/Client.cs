using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    public class Client
    {
        /// <summary>
        /// 客户端ID,主键
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 客户端密码
        /// </summary>
        public virtual string Secret { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 访问接口的客户端类型，主要区分js客户端和非js客户端
        /// </summary>
        public virtual Enums.ApplicationTypes ApplicationType { get; set; }
        /// <summary>
        /// 客户端是否还有效
        /// </summary>
        public virtual bool Active { get; set; }
        /// <summary>
        /// 客户端的访问时效
        /// </summary>
        public virtual int RefreshTokenLifeTime { get; set; }
        /// <summary>
        /// 准许客户端来源，主要用于验证js客户端，非js客户端为"*"
        /// </summary>
        public virtual string AllowedOrigin { get; set; }
    }
}
