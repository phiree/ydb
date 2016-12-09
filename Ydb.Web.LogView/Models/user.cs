using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dianzhu.Web.Log.Models
{
    public class user
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        public String username { get; set; }
        public String password { get; set; }
    }
}