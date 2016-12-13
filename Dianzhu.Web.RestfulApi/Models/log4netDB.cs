using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Ydb.Common.Infrastructure;

namespace Dianzhu.Web.RestfulApi.Models
{
    public class log4netDB
    {
        //For Best practice, Please put this in the web.config. This is only for demo purpose.
        //====================================================
        public String connectionString = "mongodb://localhost";
        //public String connectionString = System.Configuration.ConfigurationManager
        //        .ConnectionStrings["MongoDB"].ConnectionString;
        public String DataBaseName = "log4net";
        //====================================================
        private IMongoDatabase Database;
        public log4netDB(IEncryptService encryptService, string strConn)
        {
            connectionString = string.IsNullOrEmpty(strConn) ? connectionString : encryptService.Decrypt(strConn, false);
            //connectionString = "mongodb://jsyk2016:qwe.20161209@localhost/log4net";
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(DataBaseName);
        }

        public IMongoCollection<ApiInfo> ApiInfos
        {
            get
            {
                return Database.GetCollection<ApiInfo>("ApiInfos");//不填"ApiInfos"，则默认为ApiInfos
            }
        }


        public IMongoCollection<log> logs
        {
            get
            {
                return Database.GetCollection<log>("logs");
            }
        }

        public IMongoCollection<user> users
        {
            get
            {
                return Database.GetCollection<user>("users");
            }
        }
    }
}