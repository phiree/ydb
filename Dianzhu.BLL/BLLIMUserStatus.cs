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
        public IDAL.IDALIMUserStatus DALIMUserStatus;

        public BLLIMUserStatus(IDAL.IDALIMUserStatus dal)
        {
            DALIMUserStatus = dal;
        }

        public void Save(IMUserStatus im)
        {
            DALIMUserStatus.Add(im);
        }

        public void Update(IMUserStatus im)
        {
            DALIMUserStatus.Update(im);
        }

        public IMUserStatus GetIMUSByUserId(Guid userId)
        {
            return DALIMUserStatus.GetIMUSByUserId(userId);
        }

        public IList<IMUserStatus> GetOnlineListByClientName(string name)
        {
            return DALIMUserStatus.GetOnlineListByClientName(name);
        }
    }
}
