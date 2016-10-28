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
    class OrderShareServiceTest
    {
        IOrderShareService orderShareService;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            orderShareService = Bootstrap.Container.Resolve<IOrderShareService>();
        }

        [Test()]
        public void ShareOrderTest()
        {
            OrderShareParam order = new OrderShareParam {
                RelatedObjectId = "0763ec35-e349-425f-8217-a69b0114bb1e",
                Amount = 0.2m,
                ServiceTypeID = "5d459b5d-316f-4795-9872-aaf3ab30d94b",
                BalanceUser= new List<BalanceUserParam> {
                    new BalanceUserParam { AccountId = "9f6794b5-6344-4445-a941-a64400c2bac6", UserType = "customerservice" },
                    new BalanceUserParam { AccountId = "c64d9dda-4f6e-437b-89d2-a591012d8c65", UserType = "diandian" }
                }
            };
            orderShareService.ShareOrder(order);
            Console.WriteLine("OrderShareServiceTest.ShareOrderTest:分账成功！");
        }
    }
}
