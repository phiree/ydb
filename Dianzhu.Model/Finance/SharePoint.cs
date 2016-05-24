using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model.Finance
{
    /// <summary>
    /// 分成比例
    /// </summary>
    public class SharePoint
    {
<<<<<<< HEAD
        public static SharePoint None {
            get { return new SharePoint(); }
        }
        public Guid Id { get; set; }
        public DZMembership Membership { get; set; }
        public decimal Point { get; set; }
=======
      
        public static SharePoint None {
            get { return new SharePoint(); }
        }
        public virtual Guid Id { get; set; }
        public virtual DZMembership Membership { get; set; }
        public virtual decimal Point { get; set; }
>>>>>>> feature/finance

        /// <summary>
        /// 该用户的分成比例
        /// </summary>
        /// <param name="defaultSharePoint"></param>
        /// <returns></returns>
         
    }
    public class DefaultSharePoint
    {
<<<<<<< HEAD
        public static DefaultSharePoint None {
            get { return new DefaultSharePoint(); }
        }
        public Guid Id { get; set; }
        public Dianzhu.Model.Enums.enum_UserType UserType;
        public decimal Point { get; set; }
=======
        protected DefaultSharePoint() { }
        public DefaultSharePoint(decimal point, Dianzhu.Model.Enums.enum_UserType UserType)
        {
            this.Point = point;
            this.UserType = UserType;
        }
        public static DefaultSharePoint None {
            get { return new DefaultSharePoint(0.0m, Enums.enum_UserType.admin); }
        }
        public virtual Guid Id { get; set; }
        public virtual Dianzhu.Model.Enums.enum_UserType UserType { get; set; }
        public virtual decimal Point { get; set; }
>>>>>>> feature/finance
    }
}
