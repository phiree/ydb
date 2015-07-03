using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.DAL;
using Dianzhu.BLL;
using NUnit.Framework;
using Rhino.Mocks;

using FluentNHibernate.Testing;
using NHibernate;
using TestStack.Dossier.Factories;
using FizzWare.NBuilder;
 

namespace Dianzhu.Test
{
      [TestFixture]
    public class cashticket_generator_test
    {
          Business b1;
          Business b2;
          CashTicketAssigner assigner=new CashTicketAssigner();
          Dictionary<Business, IList<Business>> neighbours;
          [SetUp]
          public void build_data()
          {
              //构建商户1
               b1= Builder<Business>.CreateNew()
                 .With(x => x.Latitude = 42.1719404)
                 .With(x => x.Longitude = 128.8878797)
                 .With(x=>x.PromoteScope=1)
                 .Build();
              //    商户1的现金券模板
             IList<CashTicketTemplate> tc_list1 = Builder<CashTicketTemplate>
                 .CreateListOfSize(10).All().With(x => x.Business = b1).With(x=>x.Enabled=true).Build();
              //        商户1的现金券
             foreach (CashTicketTemplate tc in tc_list1)
             {

                 IList<CashTicket> ctl = Builder<CashTicket>.CreateListOfSize(100).All()
                     .With(x=>x.CashTicketTemplate=tc).Build();
                 tc.CashTickets = ctl;
            
             }
             b1.CashTicketTemplates = tc_list1;
             //2015/7/2 10:09:43P1:42.1714405,128.8878797   P2:42.1719405,128.8878797; RESULT:55.6454598758562
               b2 = Builder<Business>.CreateNew()
                 .With(x => x.Latitude = 42.1714405)
                 .With(x => x.Longitude = 128.8878797)
                 .With(x => x.PromoteScope = 100)
                 .Build();

             IList<CashTicketTemplate> tc_list2 = Builder<CashTicketTemplate>
                .CreateListOfSize(10).All().With(x => x.Business = b2).With(x => x.Enabled = true).Build();
             //        商户1的现金券
             foreach (CashTicketTemplate tc in tc_list2)
             {

                 IList<CashTicket> ct2 = Builder<CashTicket>.CreateListOfSize(100).All()
                     .With(x => x.CashTicketTemplate = tc).Build();
                 tc.CashTickets = ct2;

             }
             b2.CashTicketTemplates = tc_list2;

             var bl = new List<Business>();
             bl.Add(b1);
             bl.Add(b2);
             assigner.BusinessList = bl;
               neighbours = assigner.FindNeighbour(bl);
          }

          [Test]
          public void find_neighbours_test()
          { 
             
             
             Assert.AreEqual(1,neighbours[b2].Count);
             Assert.AreEqual(0,neighbours[b1].Count);
          }
          [Test]
          public void assign_test()
          {
              assigner.AssignForOne(b1, neighbours[b1], 0.2m);//b1 开始分配
              Assert.AreEqual(200, b1.CashTickets.Where(x => x.BusinessAssigned == b1).Count());
              Assert.AreEqual(800, b1.CashTickets.Where(x => x.BusinessAssigned == null).Count());
              assigner.AssignForOne(b2, neighbours[b2], 0.4m);//b2 开始分配

              Assert.AreEqual(400, b2.CashTickets.Where(x => x.BusinessAssigned == b2).Count());
              Assert.AreEqual(600, b2.CashTickets.Where(x => x.BusinessAssigned == b1).Count());

              b1.PromoteScope = 100;
              assigner.AssignForOne(b1, neighbours[b1], 0.3141m);
              Assert.AreEqual(310, b1.CashTickets.Where(x => x.BusinessAssigned == b1).Count());
              Assert.AreEqual(690, b1.CashTickets.Where(x => x.BusinessAssigned == null).Count());

                
          }
          //部分优惠券被领取之后的 重新分配.
          [Test]
          public void assign_test_after_picked()
          {
              DZMembership dz = Builder<DZMembership>.CreateNew().Build();
              foreach (CashTicketTemplate ctt in b2.CashTicketTemplates)
              {
                  ctt.CashTickets.Take(10).ToList().ForEach(x => x.UserAssigned = dz);
              }
              assigner.AssignForOne(b2, neighbours[b2], 0.3m);
              Assert.AreEqual(270, b2.CashTickets.Where(x => x.BusinessAssigned == b2).Count());
              Assert.AreEqual(630, b2.CashTickets.Where(x => x.BusinessAssigned == b1).Count());
          }

          
    }
}
