using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.IDAL;
using Dianzhu.Model;
using System.Web.Security;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 
    /// </summary>
   public  class BLLBusiness
    {
        IDALBusiness idal = null;
        IDALMembership iDalMembership = null;
       
        public BLLBusiness(IDALBusiness idal,IDALMembership iDalMembership)
        {
            this.idal = idal;
            this.iDalMembership = iDalMembership;
        }
        public BLLBusiness()
            : this(new DALBusiness(),new DAL.DALMembership())
        { }
       /// <summary>
       /// 跨两个实体类的逻辑 传入dal 还是 bll?
       /// </summary>
       /// <param name="address"></param>
       /// <param name="description"></param>
       /// <param name="latitude"></param>
       /// <param name="longtitude"></param>
       /// <param name="name"></param>
       /// <param name="mobilePhone"></param>
       /// <param name="password"></param>
        public void Register(string address,string description
                            ,double latitude,double longtitude,string name,string mobilePhone,string password
            )
        {
            Business b = new Business { Address=address,
             Description=description, Latitude=latitude,Longitude=longtitude, Name=name};

                idal.DalBase.Save(b);
                BusinessUser bu = iDalMembership.CreateBusinessUser(mobilePhone, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"), b);
             
             
        }
       
    }
}
