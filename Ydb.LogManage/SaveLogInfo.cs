using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Ydb.Common.Infrastructure;
using SharpTestsEx;

namespace Ydb.LogManage
{
    public class SaveLogInfo
    {
        private readonly log4netDB db = new log4netDB(Bootstrap.Container.Resolve<IEncryptService>(), System.Configuration.ConfigurationManager
                  .ConnectionStrings["MongoDB"].ConnectionString);

        public void SaveLog(IList<log> logList)
        {
            var logs = db.logs;
            logs.InsertManyAsync(logList);
            for (int i = 0; i < logList.Count; i++)
            {
                if (logList[i].logger2 == "Rule.v1.RestfulApi.Web.Dianzhu")
                {
                    string strApiRoute = logList[i].logger.Replace("Ydb.", "").Replace(".Rule.v1.RestfulApi.Web.Dianzhu", "");
                    IncApiCount(strApiRoute);
                    AddRequestInfos(strApiRoute, logList[i].message);
                }
            }
            
        }

        public void SaveLastTime(string pathName,string lastTime)
        {
            var collection = db.GetCollection("SaveLastTimes");
            var doc = new BsonDocument();
            doc.Add("PathName", pathName);
            doc.Add("LastTime", lastTime);
            var filter = Builders<BsonDocument>.Filter.Eq("PathName", pathName);
            var updated = Builders<BsonDocument>.Update.Set("LastTime", lastTime);
            long c = collection.CountAsync(filter).Result;
            if (c == 0)
            {
                collection.InsertOneAsync(doc);
            }
            else
            {
                collection.UpdateOneAsync(filter, updated);
            }
        }

        public IList<BsonDocument> GetDocuments(string strCollectionName, string strFieldName,string strFieldValue)
        {
            var collection = db.GetCollection(strCollectionName);
            var buildersFilter = Builders<BsonDocument>.Filter;
            var filter = buildersFilter.Empty;
            if (!string.IsNullOrEmpty(strFieldName))
            {
                filter=filter & buildersFilter.Eq(strFieldName, strFieldValue);
            }
            //var filter = Builders<BsonDocument>.Filter.Eq(strFieldName, strFieldValue);
            var documents = collection.Find(filter).ToListAsync().Result;
            return documents;
        }

        public void FindLastTimes(Dictionary<string, string> lastTime)
        {
            //var collection = db.GetCollection("SaveLastTimes");
            //var documents = collection.Find(new BsonDocument()).ToListAsync().Result;
            var documents = GetDocuments("SaveLastTimes","","");
            foreach (var doc in documents)
            {
                lastTime.Add(doc.GetElement("PathName").Value.AsString, doc.GetElement("LastTime").Value.AsString);
            }
        }

        private void IncApiCount(string apiRoute)
        {
            var collection = db.GetCollection("ApiInfos");
            var buildersFilter = Builders<BsonDocument>.Filter.Eq("ApiRoute", apiRoute);
            var update = Builders<BsonDocument>.Update.Inc("ApiRequestNum", 1);
            var result = collection.UpdateOneAsync(buildersFilter, update).Result;
        }

        private void AddRequestInfos(string apiRoute, string message)
        {
            var collection = db.GetCollection("RequestInfos");
            var doc = new BsonDocument();
            string[] strM = message.Split(',');
            doc.Add("ApiRoute", apiRoute);
            doc.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            doc.Add("UserName", strM[1].Replace("UserName=", ""));
            doc.Add("UserId", strM[2].Replace("UserId=", ""));
            doc.Add("UserType", strM[3].Replace("UserType=", ""));
            int ind = message.IndexOf("RequestMethodUriSign=");
            if (ind >= 0)
                doc.Add("RequestMethodUriSign", message.Substring(ind).Replace("RequestMethodUriSign=", ""));
            collection.InsertOneAsync(doc);
        }


    }
}
