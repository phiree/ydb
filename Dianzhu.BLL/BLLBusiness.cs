using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using System.Device.Location;
using Dianzhu.Model;
using System.Web.Security;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace Dianzhu.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class BLLBusiness
    {
         IDAL.IDALBusiness dalBusiness;
        IDAL.IDALMembership dalMembership;
        public BLLBusiness(IDAL.IDALBusiness dalBusiness,
        IDAL.IDALMembership dalMembership)
        {
            this.dalMembership = dalMembership;
            this.dalBusiness = dalBusiness;
        }
         

        public IList<Business> GetAll()
        {
            return dalBusiness.Find(x => true);
        }
        public Business GetOne(Guid id)
        {
            return dalBusiness.FindById(id);
        }
        public void Update(Business business)
        {
            dalBusiness.Update(business);
        }
        public void Delete(Business business)
        {
            dalBusiness.Delete(business);
        }
        public void Add(Business business)
        {
            dalBusiness.Add(business);
        }
        
        
        public IList<Area> GetAreasOfBusiness()
        {
            return dalBusiness.GetDistinctAreasOfBusiness();
        }
        public IList<Business> GetBusinessInSameCity(Area area)
        {
            return dalBusiness.GetBusinessInSameCity(area);
        }
        public void SaveList(IList<Business> businesses)
        {
            dalBusiness.SaveList(businesses);
        }
        public Business GetBusinessByPhone(string phone)
        {
            return dalBusiness.FindOne(x => x.Phone == phone);
        }
        public Business GetBusinessByEmail(string email)
        {
            return dalBusiness.FindOne(x => x.Email == email);
        }

        public int GetEnableSum(DZMembership member)
        {
           // return DALBusiness.GetEnableSum(member);
            // x.Owner == member).And(x => x.Enabled == true)
            Expression<Func<Model.Business, bool>> sameOwner = i => i.Owner.Id ==member.Id;
            Expression<Func<Model.Business, bool>> isEnabled = i => i.Enabled;

            int result=(int) dalBusiness.GetRowCount(DDDCommon.SpecExprExtensions.And(sameOwner,isEnabled) );
            return result;
        }

        /// <summary>
        /// 解析传递过来的 string, 
        /// </summary>
        public IList<Business> GetBusinessListByOwner(Guid memberId)
        {
            return dalBusiness.Find(x => x.Owner.Id == memberId);
       //     return DALBusiness.GetBusinessListByOwner(memberId);
        }
        //如果图片保存不是通过编辑 Business 对象来完成的(比如 通过ajax mediaserver)
 
       

        public Business GetBusinessByIdAndOwner(Guid id, Guid ownerId)
        {
            return dalBusiness.FindOne(x => x.Id == id && x.Owner.Id == ownerId);
          //  return DALBusiness.GetBusinessByIdAndOwner(id, ownerId);
        }
        public IList<Business> GetListByPage(int pageIndex, int pageSize, out long totalRecord)
        {
            return dalBusiness.Find(x => true, pageIndex, pageSize, out totalRecord);
        }
    }

}
