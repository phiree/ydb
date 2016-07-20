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
using DDDCommon;
using System.Reflection;
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

            var where = PredicateBuilder.And(sameOwner, isEnabled);
            int result=(int) dalBusiness.GetRowCount(where);
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

        /// <summary>
        /// 条件读取店铺
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="strName"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public IList<Business> GetStores(Trait_Filtering filter, string strName, string UserID)
        {
            var where = PredicateBuilder.True<Business>();
            
            if (!string.IsNullOrEmpty(UserID))
            {
                where = where.And(x => x.Owner.Id == new Guid(UserID));
            }
            if (!string.IsNullOrEmpty(strName))
            {
                where = where.And(x => x.Name.Contains(strName));
            }
            Business baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = dalBusiness.FindById(new Guid(filter.baseID));
                }
                catch
                {
                    baseone = null;
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? dalBusiness.Find(where,filter.sortby, filter.ascending,filter.offset,baseone).ToList() : dalBusiness.Find(where,filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending,filter.offset, baseone).ToList();
            return list;
        }

        /// <summary>
        /// 统计店铺数量
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public long GetStoresCount(string alias, Guid ownerId)
        {
            var where = PredicateBuilder.True<Business>();
            if (ownerId != Guid.Empty)
            {
                where = where.And(x => x.Owner.Id == ownerId);
            }
            if (!string.IsNullOrEmpty(alias))
            {
                where = where.And(x => x.Name.Contains(alias));
            }
            long count = dalBusiness.GetRowCount(where);
            return count;
        }
    }

}
