using System.Collections.Generic;
using Ydb.BusinessResource.DomainModel;

namespace Dianzhu.ApplicationService.Staff
{
    public interface IStaffService
    {
        void changeObj(staffObj staffobj, Ydb.BusinessResource.DomainModel.Staff staff);
        object DeleteStaff(string storeID, string staffID, Customer customer);
        staffObj GetStaff(string storeID, string staffID);
        IList<staffObj> GetStaffs(string storeID, common_Trait_Filtering filter, common_Trait_StaffFiltering stafffilter, Customer customer);
        countObj GetStaffsCount(string storeID, common_Trait_StaffFiltering stafffilter, Customer customer);
        staffObj PatchStaff(string storeID, string staffID, staffObj staffobj, Customer customer);
        staffObj PostStaff(string storeID, staffObj staffobj, Customer customer);
    }
}