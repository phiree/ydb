using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ydb.Membership.DomainModel;
using Ydb.Common.Infrastructure;
namespace Ydb.Infrastructure
{
  
   public class DownloadAvatarToMediaServer: IDownloadAvatarToMediaServer
    {
        IHttpRequest httpRequest;
        public DownloadAvatarToMediaServer(IHttpRequest httpRequest)
        {
            this.httpRequest = httpRequest;
        }
        public   string DownloadToMediaserver(string fileUrl, string strOriginalName, string strFileType)
        {
            //string url = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl");
            //var respData = new NameValueCollection();
            //respData.Add("fileUrl", HttpUtility.UrlEncode(fileUrl));
            //respData.Add("originalName", string.Empty);
            //respData.Add("domainType", "StaffAvatar");
            //respData.Add("fileType", "image");
            try
            {
                string url = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrlByDate");
                //url = "http://192.168.1.177:8038/UploadFileByDate.ashx";
                var respData = new NameValueCollection();
                string strUrl = HttpUtility.UrlEncode(fileUrl);
                if (fileUrl.IndexOf(':') == 1)
                {
                    respData.Add("fileBase64", ToBase64(fileUrl));
                }
                else
                {
                    respData.Add("fileUrl", strUrl);
                }
                respData.Add("originalName", strOriginalName);
                respData.Add("fileType", strFileType);
                respData.Add("domainType", "StaffAvatar");
                string strResult = httpRequest.CreateHttpRequest(url.ToString(), "post", respData);
                if (strResult.Contains("上传失败！"))
                {
                    throw new Exception(strResult);
                }
                return strResult;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("上传失败！"))
                {
                    throw new Exception(ex.Message);
                }
                else
                {
                    throw new Exception("上传失败！" + ex.Message);
                }
            }
        }

        public   string ToBase64(string fileUrl)
        {
            //Image img = new Image();
            //img.ImageUrl = fileUrl;
            //BinaryFormatter binFormatter = new BinaryFormatter();
            //MemoryStream memStream = new MemoryStream();
            //binFormatter.Serialize(memStream, img);
            //byte[] bytes = memStream.GetBuffer();
            //string base64 = Convert.ToBase64String(bytes);

            string base64Img = Convert.ToBase64String(System.IO.File.ReadAllBytes(fileUrl));
            return base64Img;

        }

    }
}
