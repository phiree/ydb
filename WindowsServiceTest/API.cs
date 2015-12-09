using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;
using System.Net;
using System.IO;
namespace WindowsService.Diandian
{
   public  class API
    {
       public static Newtonsoft.Json.Linq.JObject GetApiResult(string postData)
       {
           WebRequest request = WebRequest.Create(GlobalViables.APIBaseURL);
           request.Method = "POST";

           
           byte[] byteArray = Encoding.UTF8.GetBytes(postData);
           request.ContentLength = byteArray.Length;
           request.ContentType = "application/x-www-form-urlencoded";
           Stream dataStream = request.GetRequestStream();
           dataStream.Write(byteArray, 0, byteArray.Length);
           dataStream.Close();
           // Get the response.
           WebResponse response = request.GetResponse();
           Console.WriteLine(((HttpWebResponse)response).StatusDescription);
           dataStream = response.GetResponseStream();
           // Open the stream using a StreamReader for easy access.
           StreamReader reader = new StreamReader(dataStream);
           // Read the content.
           string responseFromServer = reader.ReadToEnd();
           // Display the content.
           object c = Newtonsoft.Json.JsonConvert.DeserializeObject(responseFromServer);
             reader.Close();
           dataStream.Close();
           response.Close();

           return (Newtonsoft.Json.Linq.JObject)c;
       }
    }
}
