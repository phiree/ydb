using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel
{
    public interface IDownloadAvatarToMediaServer
    {
        string DownloadToMediaserver(string fileUrl, string strOriginalName, string strFileType);
    }
}
