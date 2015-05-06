using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dianzhu.Model
{
    /// <summary>
    /// 商家
    /// </summary>
   public class Business
    {
       public virtual Guid Id { get; set; }
       /// <summary>
       ///名称
       /// </summary>
       public virtual string Name{ get; set; }
       /// <summary>
       /// 公司地址
       /// </summary>
       public virtual string AddressOffice{ get; set; }
       /// <summary>
       /// 店铺地址
       /// </summary>
       public virtual string AddressShop { get; set; }
       /// <summary>
       /// 店铺地理坐标,精度，维度。
       /// </summary>
       public virtual double Longitude { get; set; }
       public virtual double Latitude { get; set; }
       /// <summary>
       /// 商家介绍
       /// </summary>
       public virtual string Description { get; set; }
      
       

    }
}
