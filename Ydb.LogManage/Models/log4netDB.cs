using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Linq;
using System.Collections.Generic;
using Ydb.Common.Infrastructure;

namespace Ydb.LogManage
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


        private void EnsureCollectionExists(IMongoDatabase db, string collectionName)
        {
            if (!CollectionExists(db, collectionName))
            {
                CreateCollection(db, collectionName);
            }
        }

        private bool CollectionExists(IMongoDatabase db, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);

            return db.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter })
                     .Result
                     .ToListAsync()
                     .Result
                     .Any();
        }

        private void CreateCollection(IMongoDatabase db, string collectionName)
        {
            var cob = new CreateCollectionOptions();
            db.CreateCollectionAsync(collectionName, cob).GetAwaiter().GetResult();
        }


        public IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            EnsureCollectionExists(Database, collectionName);
            return Database.GetCollection<BsonDocument>(collectionName);
        }

        public IMongoCollection<log> logs
        {
            get
            {
                return Database.GetCollection<log>("logs");
            }
        }

        //public IMongoCollection<SaveLastTime> SaveLastTimes
        //{
        //    get
        //    {
        //        return Database.GetCollection<SaveLastTime>("SaveLastTimes");
        //    }
        //}



    }
}