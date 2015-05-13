using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.IDAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
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

        public void Register(string address,string description
                            ,double latitude,double longtitude,string name,string mobilePhone,string password
            )
        {
            Business b = new Business { Address=address,
             Description=description, Latitude=latitude,Longitude=longtitude, Name=name};

                idal.DalBase.Save(b);
                BusinessUser bu = iDalMembership.CreateBusinessUser(mobilePhone, password, b);
             
             
        }
       
    }
}
