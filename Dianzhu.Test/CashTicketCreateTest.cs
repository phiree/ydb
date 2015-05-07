using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.DAL;
using Dianzhu.BLL;
using NUnit.Framework;
using Rhino.Mocks;
using Dianzhu.IDAL;
namespace Dianzhu.Test
{
    [TestFixture]
    public class CashTicketCreateTest
    {
        [Test]
        public void CreateTicketTemplate()
        {
            var dal = MockRepository.GenerateStub<IDALCashTicketTemplate>();
            var dalBase = MockRepository.GenerateStub<IDALBase<CashTicketTemplate>>();
           
            CashTicketTemplate tt=new CashTicketTemplate{
             Amount=10,
             Conditions="饮料除外",
              Coverage=1.4f,
               Enabled=true,
                ExpiredDate=DateTime.Now.AddDays(30),
                 Name="琳琅现金券10元",
                  Owner=new Business(),
                   ValidDate=DateTime.Today.AddDays(1)
            };

            dalBase.Stub(x => x.Save(tt));
            dal.DalBase = dalBase;
            BLLCashTicketTemplate bll = new BLLCashTicketTemplate(dal);
            
             CashTicketTemplate ttt=bll.Create("琳琅现金券10元", new Business(), DateTime.Now, DateTime.Now.AddDays(10)
                , 10, "饮料除外",1.4f);
             Assert.AreEqual(ttt.Amount, 10);
        }
    }
}
