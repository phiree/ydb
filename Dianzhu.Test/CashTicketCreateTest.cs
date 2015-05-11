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
using NUnit.Framework;
using FluentNHibernate.Testing;
using NHibernate;
namespace Dianzhu.Test
{
    [TestFixture]
    public class CashTicketCreateTest
    {
        ISession session = null;
        [SetUp]
        public void SetUp()
        {
             session = new HybridSessionBuilder().GetSession();
        }
       
        [Test]
        public void CanCorrectlyMapCashTicketTemplate()
        {
           
            new PersistenceSpecification<CashTicketTemplate>(session)
                //.CheckProperty(c => c.Id, new Guid("06f5d2bf-4499-4575-8945-a49200c30a0f"))
                .CheckProperty(c => c.Amount, 10)
                .CheckProperty(c => c.Conditions,"No Limited")
                .CheckProperty(c=>c.Owner,null)
                .CheckProperty(c => c.Coverage, 1.4f)
               // .CheckProperty(c => c.Enabled, false)
               // .CheckProperty(c => c.ExpiredDate, DateTime.Now.AddMonths(1))
                .CheckProperty(c => c.Name,"CashTicketTemplate1")
               // .CheckProperty(c => c.ValidDate, DateTime.Now.AddDays(1))
                
                .VerifyTheMappings();
        }
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
