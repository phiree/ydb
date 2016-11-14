using NUnit.Framework;
using Ydb.Finance.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Specification;

namespace Ydb.Finance.Tests.Application
{
    [TestFixture()]
    public class WithdrawApplyServiceTests
    {
        IWithdrawApplyService withdrawApplyService;
        IOrderShareService orderShareService;
        IServiceTypePointService serviceTypePointService;
        IUserTypeSharePointService userTypeSharePointService;
        IBalanceAccountService balanceAccountService;
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            withdrawApplyService = Bootstrap.Container.Resolve<IWithdrawApplyService>();
            orderShareService = Bootstrap.Container.Resolve<IOrderShareService>();
            serviceTypePointService = Bootstrap.Container.Resolve<IServiceTypePointService>();
            userTypeSharePointService = Bootstrap.Container.Resolve<IUserTypeSharePointService>();
            balanceAccountService = Bootstrap.Container.Resolve<IBalanceAccountService>();
        }

        /// <summary>
        /// 绑定提现账号
        /// </summary>
        [Test()]
        public void WithdrawApplyService_SaveWithdrawApply_ForAddOne()
        {
            WithdrawApplyDto withdrawApplyDto = SaveWithdrawApply();
            Console.WriteLine("BalanceAccountService.BindingAccount:"+ withdrawApplyDto.Id.ToString());
        }
        private WithdrawApplyDto SaveWithdrawApply()
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
            OrderShareParam order = new OrderShareParam
            {
                RelatedObjectId = "0763ec35-e349-425f-8217-a69b0114bb1e",
                SerialNo = "FW2016092117251802701",
                BusinessUserId = "09ccc183-ed87-462a-8d11-a66600fbbd24",
                Amount = 16,
                ServiceTypeID = "003fa8eb-3649-4ddd-8c7d-9028c0f6a94f",
                BalanceUser = new List<BalanceUserParam> {
                    new BalanceUserParam { AccountId = "9f6794b5-6344-4445-a941-a64400c2bac6", UserType = "customerservice" }
                }
            };
            orderShareService.ShareOrder(order);
            WithdrawApplyDto withdrawApplyDto = withdrawApplyService.SaveWithdrawApply("09ccc183-ed87-462a-8d11-a66600fbbd24", "jsyk_development@126.com", AccountTypeEnums.Alipay, 1.1m, "WA2016092117251802701");
            Console.WriteLine("BalanceAccountService.BindingAccount:申请成功！");
            return withdrawApplyDto;
        }

        /// <summary>
        /// 取消申请提现
        /// </summary>
        [Test()]
        public void WithdrawApplyService_ConcelWithdrawApply_ByApplyId()
        {
            WithdrawApplyDto withdrawApplyDto = SaveWithdrawApply();
            withdrawApplyService.ConcelWithdrawApply(withdrawApplyDto.Id);
            Console.WriteLine("BalanceAccountService.BindingAccount:取消成功！");
        }

        /// <summary>
        /// 根据条件获取提现申请
        /// </summary>
        [Test()]
        public void WithdrawApplyService_GetWithdrawApplyList()
        {
            TraitFilter traitFilter = new TraitFilter();
            WithdrawApplyFilter withdrawApplyFilter = new WithdrawApplyFilter();
            IList<WithdrawApplyDto> withdrawApplyDtoList = withdrawApplyService.GetWithdrawApplyList(traitFilter, withdrawApplyFilter);
            Console.WriteLine("BalanceAccountService.GetWithdrawApplyList:" + withdrawApplyDtoList.Count );
        }

        /// <summary>
        /// 支付操作
        /// </summary>
        [Test()]
        public void WithdrawApplyService_PayByWithdrawApply()
        {
            string errStr = "";
            IList<Guid> guidApplyId = new List<Guid>();
            TraitFilter traitFilter = new TraitFilter();
            WithdrawApplyFilter withdrawApplyFilter = new WithdrawApplyFilter();
            withdrawApplyFilter.ApplyStatus = ApplyStatusEnums.ApplyWithdraw;
            IList<WithdrawApplyDto> withdrawApplyDtoList = withdrawApplyService.GetWithdrawApplyList(traitFilter, withdrawApplyFilter);
            for (int i = 0; i < withdrawApplyDtoList.Count; i++)
            {
                guidApplyId.Add(withdrawApplyDtoList[i].Id);
            }
            IList<WithdrawCashDto> withdrawCashDtoList = withdrawApplyService.PayByWithdrawApply(guidApplyId, "", "PA2016092117251802701",out errStr);
            Console.WriteLine("BalanceAccountService.PayByWithdrawApply:" + withdrawCashDtoList.Count);
        }
    }
}
