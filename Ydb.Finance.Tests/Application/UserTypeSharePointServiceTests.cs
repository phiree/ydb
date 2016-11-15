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
        public void UserTypeSharePointService_Add_AddOneUserTypeSharePoint()
        {
            UserTypeSharePointDto dPoint = userTypeSharePointService.GetSharePointInfo("customerservice");
            if (dPoint == null)
            {
                userTypeSharePointService.Add("customerservice", 0.35m);
            }
            userTypeSharePointService.Add("agent", 0.4m);
            Console.WriteLine("UserTypeSharePointService.Add:添加成功！");
        }

        /// <summary>
        /// 修改一条用户类型分配比例
        /// </summary>
        [Test()]
        public void UserTypeSharePointService_Update_UpdateOneUserTypeSharePoint()
        {
            UserTypeSharePointDto dPoint = userTypeSharePointService.GetSharePointInfo("customerservice");
            if (dPoint == null)
            {
                userTypeSharePointService.Add("customerservice", 0.35m);
            }
            userTypeSharePointService.Update("customerservice", 0.36m);
            Console.WriteLine("UserTypeSharePointService.Update:修改成功！");
        }

        /// <summary>
        /// 根据用户类型获取该用户类型的分配比例
        /// </summary>
        [Test()]
        public void UserTypeSharePointService_GetSharePoint_ByUserType()
        {
            UserTypeSharePointDto dPointDto = userTypeSharePointService.GetSharePointInfo("customerservice");
            if (dPointDto == null)
            {
                userTypeSharePointService.Add("customerservice", 0.35m);
            }
            string errMsg = "";
            decimal dPoint = userTypeSharePointService.GetSharePoint("customerservice", out errMsg);
            Console.WriteLine("UserTypeSharePointService.GetSharePoint:" + dPoint);
            if (errMsg != "")
            {
                Console.WriteLine("UserTypeSharePointService.GetSharePoint:" + errMsg);
            }
        }

        /// <summary>
        /// 根据用户类型获取该用户类型的分配比例信息
        /// </summary>
        [Test()]
        public void UserTypeSharePointService_GetSharePointInfo_ByUserType()
        {
            UserTypeSharePointDto dPoint = userTypeSharePointService.GetSharePointInfo("customerservice");
            Console.WriteLine("UserTypeSharePointService.GetSharePointInfo:" + (dPoint==null).ToString());
        }

        /// <summary>
        /// 获取所有的用户类型分配比例
        /// </summary>
        [Test()]
        public void UserTypeSharePointService_GetAll()
        {
            var list = userTypeSharePointService.GetAll();
            Console.WriteLine("UserTypeSharePointService.GetAll:" + list.Count);
        }

        /// <summary>
        /// 根据Id获取一条用户类型分配比例信息
        /// </summary>
        [Test()]
        public void UserTypeSharePointService_GetOne()
        {
            UserTypeSharePointDto dPoint = userTypeSharePointService.GetOne(new Guid("d3182919-2129-4e38-87b8-a66e00c08001"));
            Console.WriteLine("UserTypeSharePointService.GetOne:" + (dPoint == null).ToString());
        }
    }
}
