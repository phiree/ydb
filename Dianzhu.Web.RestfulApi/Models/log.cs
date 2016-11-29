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
        public String threadContextProperty { get; set; }
        public String globalContextProperty { get; set; }
        public String numberProperty { get; set; }
        public String dateProperty { get; set; }
        public String exception { get; set; }
        public String customProperty { get; set; }

    }
}