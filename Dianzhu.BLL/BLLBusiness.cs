using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;

using Dianzhu.Model;
using System.Web.Security;
namespace Dianzhu.BLL
{
    /// <summary>
    /// 
    /// </summary>
   public  class BLLBusiness
    {
        public DALBusiness DALBusiness = DALFactory.DALBusiness;
        public DALMembership dalMembership = DALFactory.DALMembership;

        public void Register(string address,string description
                            ,double latitude,double longtitude,string name,string mobilePhone,string password
            )
        {
            Business b = new Business { Address=address,
             Description=description, Latitude=latitude,Longitude=longtitude, Name=name,
             DateApply=(DateTime)System.Data.SqlTypes.SqlDateTime.MinValue,
                                        DateApproved = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue
            };

                DALBusiness.Save(b);
                BusinessUser bu = dalMembership.CreateBusinessUser(mobilePhone, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"), b);
             
             
        }

        public IList<Business> GetAll()
        {
            return DALBusiness.GetAll<Business>();
        }
        public Business GetOne(Guid id)
        {
            return DALBusiness.GetOne(id);
        }
        public void Updte(Business business)
        {
            DALBusiness.Update(business);
        }
        public void Delete(Business business)
        {
            DALBusiness.Delete(business);
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
            return DALBusiness.GetList(query, pageIndex, pageSize, out totalRecords);
        }
       
       
    }
}
