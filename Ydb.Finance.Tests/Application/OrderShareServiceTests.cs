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
    class OrderShareServiceTests
    {
        IOrderShareService orderShareService;
        IServiceTypePointService serviceTypePointService;
        IUserTypeSharePointService userTypeSharePointService;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            orderShareService = Bootstrap.Container.Resolve<IOrderShareService>();
            serviceTypePointService = Bootstrap.Container.Resolve<IServiceTypePointService>();
            userTypeSharePointService = Bootstrap.Container.Resolve<IUserTypeSharePointService>();
        }

        /// <summary>
        /// 分账操作
        /// </summary>
        [Test()]
        public void OrderShareService_ShareOrder()
        {
            UserTypeSharePointDto userTypeSharePointDto = userTypeSharePointService.GetSharePointInfo("customerservice");
            if (userTypeSharePointDto == null)
            {
                userTypeSharePointService.Add("customerservice", 0.35m);
            }
            ServiceTypePointDto serviceTypePointDto = serviceTypePointService.GetPointInfo("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f");
            if (serviceTypePointDto == null)
            {
                serviceTypePointService.Add("003fa8eb-3649-4ddd-8c7d-9028c0f6a94f", 0.08m);
            }
            OrderShareParam order = new OrderShareParam {
                RelatedObjectId = "0763ec35-e349-425f-8217-a69b0114bb1e",
                SerialNo= "FW2016092117251802701",
                BusinessUserId= "09ccc183-ed87-462a-8d11-a66600fbbd24",
                Amount = 16,
                ServiceTypeID = "003fa8eb-3649-4ddd-8c7d-9028c0f6a94f",
                BalanceUser= new List<BalanceUserParam> {
                    new BalanceUserParam { AccountId = "9f6794b5-6344-4445-a941-a64400c2bac6", UserType = "customerservice" }
                }
            };
            orderShareService.ShareOrder(order);
            Console.WriteLine("OrderShareService.ShareOrder:分账成功！");
        }
    }
}
