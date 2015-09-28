﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
namespace MediaServer
{
    public class HttpUploader
    {
        
        public  static  string Upload(string uploaderUrl, string base64,string originalName,
            string domainType,string fileType)
        {
            string uploadedPath = string.Empty;
            originalName= Path.GetFileName(originalName);
            string postString = string.Format("fileBase64={0}&originalName={1}&domainType={2}&fileType={3}"
                , base64,originalName,domainType,fileType);


            System.Net.WebRequest request = WebRequest.Create(uploaderUrl);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postString.Length;
            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();

            StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream());

            string responseData = responseReader.ReadToEnd();

            responseReader.Close();
            request.GetResponse().Close();
            return responseData;

        }



    }
}
