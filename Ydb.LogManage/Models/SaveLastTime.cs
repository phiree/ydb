using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ydb.LogManage
{
    public class SaveLastTime
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        public String PathName { get; set; }
        public String LastTime { get; set; }
    }
}
