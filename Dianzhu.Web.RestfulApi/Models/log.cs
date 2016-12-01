using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Dianzhu.Web.RestfulApi.Models
{
    public class log
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        public String date { get; set; }
        public String level { get; set; }
        public String thread { get; set; }
        public String logger { get; set; }
        public String logger1 { get; set; }
        public String logger2 { get; set; }
        public String message { get; set; }
        public String filename { get; set; }
        public String linenumber { get; set; }
        public String classname { get; set; }
        public String domain { get; set; }
    }
}