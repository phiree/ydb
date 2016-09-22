using NUnit.Framework;
using Dianzhu.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Dianzhu.Model;
using Dianzhu.IDAL;
namespace Dianzhu.DAL.Tests
{
    [TestFixture()]
    public class DALReceptionStatusTests
    {
        IDAL.IDALReceptionStatus dalRs ;
        IDALMembership dalMembership  ;
       
        [SetUp]
        public void SetUp()
        {
            PHSuit.Logging.Config("Dianzhu.DAL.DALReceptionStatusTest");
            Bootstrap.Boot();
            NHibernateUnitOfWork.UnitOfWork.Start();

         dalRs = Bootstrap.Container.Resolve<IDAL.IDALReceptionStatus>();
         dalMembership = Bootstrap.Container.Resolve<IDALMembership>();
            
        }
        [Test()]
        public void GetRSListByDiandianTest()
        {
            Guid id = new Guid(Dianzhu.Config.Config.GetAppSetting("DiandianLoginId"));
            DZMembership diandian = dalMembership.FindById(id);
            Dictionary<int, IList<ReceptionStatus>> assignList = new Dictionary<int, IList<ReceptionStatus>>();
            for (int i = 0; i < 10; i++)
            {
                Thread t = new Thread(a =>
                {

                    assignList.Add(i, dalRs.GetRSListByDiandian(diandian, 3));

                });
            }
            foreach (var assign in assignList)
            {
                string s = assign.Key.ToString() + ":";
                foreach (ReceptionStatus rs in assign.Value)
                {
                    s += rs.Customer.UserName;
                }
                Console.WriteLine(s);

            }



        }

        [TearDown]
        public void TearDown()
        {
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush(System.Data.IsolationLevel.Unspecified);
        }

        [Test()]
        public void GetRSListByDiandianAndUpdateTest()
        {
            Guid id = new Guid(Dianzhu.Config.Config.GetAppSetting("DiandianLoginId"));
            DZMembership diandian = dalMembership.FindById(id);
            DZMembership newCS=  dalMembership.GetMemberByName("test_cs_3");
            for(int i=0;i<10;i++)
            { 
            var list= dalRs.GetRSListByDiandianAndUpdate(diandian, 3, newCS);
            Console.WriteLine("assign :" + list.Count);
            }
        }
    }
}