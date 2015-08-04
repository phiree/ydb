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
namespace Dianzhu.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class BLLBusiness
    {
        public DALBusiness DALBusiness = DALFactory.DALBusiness;
        public DALMembership dalMembership = DALFactory.DALMembership;

        /// <summary>
        /// 注册新用户. 同时注册商户并将该商户的所有者owner设置为新注册用户
        /// </summary>
        /// <param name="address"></param>
        /// <param name="description"></param>
        /// <param name="latitude"></param>
        /// <param name="longtitude"></param>
        /// <param name="name"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public DZMembership Register(string address, string description
                            , double latitude, double longtitude, string name, string username, string password
            )
        {
            //注册用户
            DZMembership member = new DZMembership { UserName=username,Password=FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5")};
            if (Regex.IsMatch(username, @".+@.+\..+"))
            {
               member.Email = username;

            }
            else
            {
                member.Phone = username;
            }
            dalMembership.Save(member);
            //注册商户
            Business b = new Business
            {
                Address = address,
                Description = description,
                Latitude = latitude,
                Longitude = longtitude,
                Name = name,
                DateApply = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue,
                DateApproved = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue,
                Owner=member
            };
            
            DALBusiness.Save(b);
           // BusinessUser bu = dalMembership.CreateBusinessUser(username, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"), b);

            return member;
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
        public IList<Business> GetList(string query, int pageIndex, int pageSize, out int totalRecords)
        {
            return DALBusiness.GetList(query, pageIndex, pageSize, out totalRecords);
        }
        public IList<Area> GetAreasOfBusiness()
        {
            return DALBusiness.GetAreasOfBusiness();
        }
        public IList<Business> GetBusinessInSameCity(Area area)
        {
            return DALBusiness.GetBusinessInSameCity(area);
        }
        public void SaveList(IList<Business> businesses)
        {
            DALBusiness.SaveList(businesses);
        }
        public Business GetBusinessByPhone(string phone)
        {
            return DALBusiness.GetBusinessByPhone(phone);
        }
        public Business GetBusinessByEmail(string email)
        {
            return DALBusiness.GetBusinessByEmail(email);
        }

        /// <summary>
        /// 解析传递过来的 string, 
        /// </summary>
        public IList<Business> GetBusinessListByOwner(Guid memberId)
        {
            return DALBusiness.GetBusinessListByOwner(memberId);
        }


    }

}
