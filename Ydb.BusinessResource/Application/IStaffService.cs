using System;
using System.Collections.Generic;
using System.Web;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;
using Ydb.Common.Application;
using Ydb.Common;
namespace Ydb.BusinessResource.Application
{
    public interface IStaffService
    {
    
        void Delete(Guid staffId);
        IList<Staff> GetAllListByBusiness(Business business);
        int GetEnableSum(Business business);
        IList<Staff> GetListByBusiness(Guid businessId, int pageIndex, int pageSize, out int totalRecord);
        Staff GetOne(Guid id);
        Staff GetOneByUserID(Guid storeID, string userID);
        Staff GetStaff(Guid storeID, Guid staffID);
        IList<Staff> GetStaffs(TraitFilter filter, string alias, string email, string phone, string sex, string specialty, string realName, Guid storeID);
        long GetStaffsCount(string alias, string email, string phone, string sex, string specialty, string realName, Guid storeID);
        void Save(Staff staff);
        //string Save(Guid StaffId, HttpPostedFile imageFile, enum_ImageType imageType);
        string Save(Guid StaffId, string imageName, enum_ImageType imageType, int size);
        void Update(Staff staff);
        /// <summary>
        /// 取消安排
        /// </summary>
        /// <returns></returns>
        void CanelAssign(string staffId);
        
    }
}