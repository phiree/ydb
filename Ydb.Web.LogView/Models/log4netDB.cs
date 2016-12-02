using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Dianzhu.Web.Log.Models
{
    public class log4netDB
    {
        //For Best practice, Please put this in the web.config. This is only for demo purpose.
        //====================================================
        public String connectionString = "mongodb://localhost";
        public String DataBaseName = "log4net";
        //====================================================
        private IMongoDatabase Database;
        public log4netDB()
        {
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
        
    }
}