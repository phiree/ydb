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
          [Test]
          public void find_neighbours_test()
          {
             Business b1= Builder<Business>.CreateNew()
                 .With(x =>x.Latitude=1)
                 .With(x=>x.Longitude=1)
                 .With(x=>x.PromoteScope=1)
                 .Build();
             Business b2 = Builder<Business>.CreateNew()
                 .With(x => x.Latitude = 3)
                 .With(x => x.Longitude = 3)
                 .With(x => x.PromoteScope = 100)
                 .Build();
              
             // bllbus assigner=new CashTicketAssigner();
             // assigner.
          }
    }
}
