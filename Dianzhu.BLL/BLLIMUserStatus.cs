using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.Model;
namespace Dianzhu.BLL
{
    public class BLLIMUserStatus
    {
        public DALIMUserStatus DALIMUserStatus = DALFactory.DALIMUserStatus;

        public void SaveOrUpdate(IMUserStatus im)
        {
            DALIMUserStatus.SaveOrUpdate(im);
        }

        public IMUserStatus GetIMUSByUserId(Guid userId)
        {
            return DALIMUserStatus.GetIMUSByUserId(userId);
        }

        public IList<IMUserStatus> GetListByClientName(string name)
        {
            return DALIMUserStatus.GetListByClientName(name);
        }
    }
}
