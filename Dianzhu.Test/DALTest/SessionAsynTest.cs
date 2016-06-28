using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NHibernate;
using Dianzhu.Model;
using System.Threading;
namespace Dianzhu.Test.DALTest
{
    [TestFixture]
    public    class SessionAsynTest
    {
        [ThreadStatic]
         static ISession session;
        [SetUp]
        public void setup()
        {
           //   session = new DAL.HybridSessionBuilder().GetSession();
        }
        [Test]
        public void TestNhibernateSessionWithMultiThread()
        {

            for (int a = 0; a < 10; a++)
            {
                Thread newThread = new Thread(() => NewThread(a));
                newThread.Start();
               
            }
        }

        private void NewThread(int threadNo)
        {
            for (int i = 0; i < 10; i++)
            {
                 ISession session = new DAL_Hyber.HybridSessionBuilder().GetSession();

                Advertisement adv = new Advertisement() { Id = Guid.NewGuid() };
                using (var tr = session.BeginTransaction())
                {
                    Console.WriteLine("thread"+ threadNo+"_" + i);
                    var existedAdv = session.Get<Advertisement>(new Guid("00066255-2973-4808-9923-a62700be4b81"));

                    existedAdv.Url = Guid.NewGuid().ToString();
                    session.Update(existedAdv);
                    tr.Commit();
                }
            }
        }
    }

    public class SessionFactory
    {
        ISession session;
        public ISession GetSession()
        {
            return new DAL_Hyber.HybridSessionBuilder().GetSession();
        }
    }
}
