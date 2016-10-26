using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ydb.Finance.Application;

namespace Ydb.Finance.Tests.Application
{
    [TestFixture()]
    public class UserTypeSharePointServiceTest
    {
        IUserTypeSharePointService userTypeSharePointService;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            userTypeSharePointService = Bootstrap.Container.Resolve<IUserTypeSharePointService>();
        }

        [Test()]
        public void AddTest()
        {
            try
            {
                userTypeSharePointService.Add("customerservice",0.35m);
                Console.WriteLine("UserTypeSharePointServiceTest.AddTest:添加成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserTypeSharePointServiceTest.AddTest:" + ex.ToString());
            }
        }

        [Test()]
        public void GetSharePointTest()
        {
            try
            {
                string errMsg = "";
                decimal dPoint = userTypeSharePointService.GetSharePoint("customerservice", out errMsg);
                Console.WriteLine("UserTypeSharePointServiceTest.GetSharePointTest:" + dPoint);
                if (errMsg != "")
                {
                    Console.WriteLine("UserTypeSharePointServiceTest.GetSharePointTest:" + errMsg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserTypeSharePointServiceTest.GetSharePointTest:" + ex.ToString());
            }
        }

        [Test()]
        public void GetAllTest()
        {
            var list = userTypeSharePointService.GetAll();
            Console.WriteLine("UserTypeSharePointServiceTest.GetAllTest:" + list.Count);
        }
    }
}
