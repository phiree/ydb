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
             Description=description, Latitude=latitude,Longitude=longtitude, Name=name,
             DateApply=(DateTime)System.Data.SqlTypes.SqlDateTime.MinValue,
                                        DateApproved = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue
            };

                idal.DalBase.Save(b);
                BusinessUser bu = iDalMembership.CreateBusinessUser(mobilePhone, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"), b);
             
             
        }

        public IList<Business> GetAll()
        {
            return idal.DalBase.GetAll<Business>();
        }
        public Business GetOne(Guid id)
        {
            return idal.DalBase.GetOne(id);
        }
        public void Updte(Business business)
        {
             idal.DalBase.Update(business);
        }
        public void Delete(Business business)
        {
            idal.DalBase.Delete(business);
        }
       /// <summary>
       /// 根据条件返回商户列表
       /// </summary>
       /// <param name="query">查询语句</param>
       /// <param name="pageIndex">分页数</param>
       /// <param name="pageSize">每页数量</param>
       /// <param name="totalRecords">符合条件的总数</param>
       /// <returns></returns>
        public IList<Business> GetList(string query,int pageIndex,int pageSize,out int totalRecords)
        {
            return idal.DalBase.GetList(query, pageIndex, pageSize, out totalRecords);
        }
       
       
    }
}
