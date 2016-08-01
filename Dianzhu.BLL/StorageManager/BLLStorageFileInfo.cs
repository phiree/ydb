using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.IDAL;
using Dianzhu.Model;

namespace Dianzhu.BLL
{
    public class BLLStorageFileInfo
    {
        IDAL.IDALStorageFileInfo dalFileInfo;

        public BLLStorageFileInfo(IDAL.IDALStorageFileInfo dalFileInfo)
        {
            this.dalFileInfo = dalFileInfo;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="c"></param>
        public void Save(StorageFileInfo fileinfo)
        {
            dalFileInfo.Add(fileinfo);
        }
    }
}
