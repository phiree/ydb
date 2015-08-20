using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Text;
using System.Drawing; 

public partial class UploadImageTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strResult = "";
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://localhost:804/FileUpLoad.ashx");

        myRequest.Method = "POST";

        myRequest.ContentType = "application/x-www-form-urlencoded";

        string base64str = getImageByteToBase64("image/ceshi.png");

        //POST提交方式编码,GET方式不需要
        string postDataStr = "filetype=img&filecode=" + HttpUtility.UrlEncode(base64str);

        Encoding encoding = Encoding.UTF8;//根据网站的编码自定义  
        byte[] postData = encoding.GetBytes(postDataStr);//postDataStr即为发送的数据，格式还是和上次说的一样  

        Response.Write(getImageByteToBase64("image/ceshi.png").Length);
        myRequest.ContentLength = postData.Length;
        Stream requestStream = myRequest.GetRequestStream();
        requestStream.Write(postData, 0, postData.Length);
        // POST提交方式编码



        HttpWebResponse HttpWResp = (HttpWebResponse)myRequest.GetResponse();

        Stream myStream = HttpWResp.GetResponseStream();
        StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
        StringBuilder strBuilder = new StringBuilder();
        while (-1 != sr.Peek())
        {
            strBuilder.Append(sr.ReadLine());
        }

        strResult = strBuilder.ToString();
        Response.Write(strResult);


    }

    private string getImageByteToBase64(string imagePath)
    {
        FileStream files = new FileStream(System.Web.HttpContext.Current.Server.MapPath(imagePath), FileMode.Open);
        byte[] imgByte = new byte[files.Length];
        files.Read(imgByte, 0, imgByte.Length);
        files.Close();
        string base64String = Convert.ToBase64String(imgByte);
        return base64String;
    }
}