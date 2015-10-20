using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALCashTicketCreateRecord : DALBase<CashTicketCreateRecord>
    {
        public DALCashTicketCreateRecord()
        {
             
        }
        //注入依赖,供测试使用;
        public DALCashTicketCreateRecord(string fortest):base(fortest)
        {
            
        }
        public IList<CashTicketCreateRecord> GetMonthRecord(Business_Abs business,int year,int month)
        {
             DateTime beginDate=new DateTime(year,month,1);
            DateTime endDate=beginDate.AddMonths(1);
            string querystring = "select r from CashTicketCreateRecord r where r.Business.Id='"
                            + business.Id + "' and r.TimeCreated between :start and :end";
            IQuery query = Session.CreateQuery(querystring);
            query.SetParameter("start", beginDate);
            query.SetParameter("end", endDate);
          return   query.List<CashTicketCreateRecord>();
        }

         
    }
}
