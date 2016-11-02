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
    public class UserTypeSharePointServiceTests
    {
        IUserTypeSharePointService userTypeSharePointService;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            userTypeSharePointService = Bootstrap.Container.Resolve<IUserTypeSharePointService>();
        }

        /// <summary>
        /// 添加一条用户类型分配比例
        /// </summary>
        [Test()]
        public void UserTypeSharePointServiceTests_AddTest_AddOneUserTypeSharePoint()
        {
            userTypeSharePointService.Add("customerservice", 0.35m);
            userTypeSharePointService.Add("agent", 0.4m);
            Console.WriteLine("UserTypeSharePointServiceTest.AddTest:添加成功！");
        }

        /// <summary>
        /// 根据用户类型获取该用户类型的分配比例
        /// </summary>
        [Test()]
        public void UserTypeSharePointServiceTests_GetSharePointTest_ByUserType()
        {
            string errMsg = "";
            decimal dPoint = userTypeSharePointService.GetSharePoint("customerservice", out errMsg);
            Console.WriteLine("UserTypeSharePointServiceTest.GetSharePointTest:" + dPoint);
            if (errMsg != "")
            {
                Console.WriteLine("UserTypeSharePointServiceTest.GetSharePointTest:" + errMsg);
            }
        }

        /// <summary>
        /// 获取所有的用户类型分配比例
        /// </summary>
        [Test()]
        public void UserTypeSharePointServiceTests_GetAllTest()
        {
            var list = userTypeSharePointService.GetAll();
            Console.WriteLine("UserTypeSharePointServiceTest.GetAllTest:" + list.Count);
        }
    }
}
