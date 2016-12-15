using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dianzhu.Web.RestfulApi.Models
{
    public class ApiInfo
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        [Required]
        public String ApiRoute { get; set; }

        [Required]
        public String ApiName { get; set; }
        
        public String ApiRule { get; set; }

        long _ApiRequestNum = 0;
        public long ApiRequestNum { get { return _ApiRequestNum; } set { _ApiRequestNum = value; } }

    }
}