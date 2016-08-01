using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell32;

namespace MediaServer
{
    public class GetMediaInfo
    {
        ///　<summary>
        ///　获取媒体文件属性信息
        ///　</summary>
        ///　<param name="path">媒体文件具体路径</param>
        ///　<param name="icolumn">具体属性的顺序值（-1简介信息 1文件大小 21时长 22比特率）</param>
        ///　<returns></returns>
        public static string GetMediaDetailInfo(string path, int icolumn)
        {
            try
            {
                ShellClass sh = new ShellClass();
                Shell32.Folder folder = sh.NameSpace(path.Substring(0, path.LastIndexOf("\\")));
                Shell32.FolderItem folderItem = folder.ParseName(path.Substring(path.LastIndexOf("\\") + 1));
                return folder.GetDetailsOf(folderItem, icolumn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
