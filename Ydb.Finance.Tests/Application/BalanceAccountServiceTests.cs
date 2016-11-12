using NUnit.Framework;
using Ydb.Finance.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Tests.Application
{
    [TestFixture()]
    public class BalanceAccountServiceTests
    {
        IBalanceAccountService balanceAccountService;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            balanceAccountService = Bootstrap.Container.Resolve<IBalanceAccountService>();
        }

        /// <summary>
        /// 绑定提现账号
        /// </summary>
        [Test()]
        public void BalanceAccountService_BindingAccount_ForAddOne()
        {
            BalanceAccountDto balanceAccountDto = new BalanceAccountDto
            {
                UserId = "09ccc183-ed87-462a-8d11-a66600fbbd24",
                Account = "jsyk_development@126.com",
                AccountName = "海口集思优科网络科技有限公司",
                AccountType = AccountTypeEnums.Alipay,
                AccountPhone = "13666666666",
                AccountCode = "333333333333333333",
                flag = 1
            };
            balanceAccountService.BindingAccount(balanceAccountDto);
            Console.WriteLine("BalanceAccountService.BindingAccount:绑定成功！");
        }

        /// <summary>
        /// 修改提现账号
        /// </summary>
        [Test()]
        public void BalanceAccountService_UpdateAccount_ForUpdateOne()
        {
            BalanceAccountDto list = balanceAccountService.GetAccount("09ccc183-ed87-462a-8d11-a66600fbbd24");
            if (list == null)
            {
                BalanceAccountDto balanceAccountDtoNew = new BalanceAccountDto
                {
                    UserId = "09ccc183-ed87-462a-8d11-a66600fbbd24",
                    Account = "jsyk_development@126.com",
                    AccountName = "海口集思优科网络科技有限公司",
                    AccountType = AccountTypeEnums.Alipay,
                    AccountPhone = "13666666666",
                    AccountCode = "333333333333333333",
                    flag = 1
                };
                balanceAccountService.BindingAccount(balanceAccountDtoNew);
            }
            BalanceAccountDto balanceAccountDto = new BalanceAccountDto
            {
                UserId = "09ccc183-ed87-462a-8d11-a66600fbbd24",
                Account = "jsyk_development1@126.com",
                AccountName = "海口集思优科网络科技有限公司1",
                AccountType = AccountTypeEnums.Alipay,
                AccountPhone = "13666666666",
                AccountCode = "333333333333333333",
                flag = 1
            };
            balanceAccountService.UpdateAccount(balanceAccountDto);
            Console.WriteLine("BalanceAccountService.BindingAccount:修改成功！");
        }

        /// <summary>
        /// 根据用户获取该用户的提现账号
        /// </summary>
        [Test()]
        public void BalanceAccountService_GetAccount_GetOneByUserId()
        {
            BalanceAccountDto list = balanceAccountService.GetAccount("09ccc183-ed87-462a-8d11-a66600fbbd24");
            Console.WriteLine("BalanceFlowService.GetBillSatistics:" + (list==null).ToString());
        }
        
    }
}
