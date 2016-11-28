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

        string _ApiRequstNum = "0";
        public String ApiRequstNum { get { return _ApiRequstNum; } set { _ApiRequstNum = value; } }

    }
}