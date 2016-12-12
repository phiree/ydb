using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Ydb.Common.Infrastructure;

namespace Dianzhu.Web.Log.Models
{
    public class log4netDB
    {
        //For Best practice, Please put this in the web.config. This is only for demo purpose.
        //====================================================
        public String connectionString = "mongodb://localhost";
        //IEncryptService encryptService ;
        //public String connectionString = encryptService.Decrypt(System.Configuration.ConfigurationManager
        //        .ConnectionStrings["MongoDB"].ConnectionString);
        public String DataBaseName = "log4net";
        //====================================================
        private IMongoDatabase Database;
        public log4netDB(IEncryptService encryptService,string strConn)
        {
            //this.encryptService = encryptService;
            connectionString = string.IsNullOrEmpty(strConn)?connectionString: encryptService.Decrypt(strConn,false);
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(DataBaseName);
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