using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Ydb.LogManage
{
    public class log
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        public DateTime timestamp { get; set; }
        public String date { get; set; }
        public String level { get; set; }
        public String thread { get; set; }
        public String logger { get; set; }
        public String logger1 { get; set; }
        public String logger2 { get; set; }
        public String message { get; set; }
        public String filename { get; set; }
        public long linenumber { get; set; }
        public String classname { get; set; }
        public String domain { get; set; }

        public void SetLogger1()
        {
            string[] strL = this.logger.Split('.');
            if (strL.Length > 4)
            {
                this.logger1 = strL[strL.Length - 4] + "." + strL[strL.Length - 3] + "." + strL[strL.Length - 2] + "." + strL[strL.Length - 1];
            }
            else
            {
                this.logger1 = this.logger;
            }
        }


        public void SetLogger2()
        {
            string[] strL = this.logger.Split('.');
            if (strL.Length > 5)
            {
                this.logger2 = strL[strL.Length - 5] + "." + strL[strL.Length - 4] + "." + strL[strL.Length - 3] + "." + strL[strL.Length - 2] + "." + strL[strL.Length - 1];
            }
            else
            {
                this.logger2 = this.logger;
            }
        }

        public void SetTimestamp()
        {
            //this.timestamp = Convert.ToDateTime(date);
            DateTime dt = DateTime.ParseExact(this.date, "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.CurrentCulture);
            this.timestamp = dt;
            //注意：string格式有要求，必须是yyyy-MM-dd hh:mm:ss
            //Convert.ToDateTime(string)

            //说明：任意格式可自定义规则。
            //DateTimeFormatInfo dtFormat = new System.GlobalizationDateTimeFormatInfo();
            //dtFormat.ShortDatePattern = "yyyy/MM/dd";
            //DateTime dt = Convert.ToDateTime("2014/10/10", dtFormat);

            //说明：任意格式可自定义规则，效果同方式二。
            //string dateString = "20141010";
            //DateTime dt = DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        }
    }
}