using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ydb.Membership.Application.Dto;
 
using FizzWare.NBuilder;
using AutoMapper;
namespace Ydb.Test.Integration
{
    [TestFixture]
   public  class Automappingtest
    {
        [Test]
        public void automaptest()
        {

            AutoMapper.Mapper.Initialize(x => {
                Membership.Application.AutoMapperConfiguration.AutoMapperMembership.Invoke(x);
                Ydb.Finance.Application.AutoMapperConfiguration.AutoMapperFinance.Invoke(x);
                 

            });

            Membership.DomainModel.DZMembership member = new Membership.DomainModel.DZMembership {  Email="email2"};

           var memebrDto= Mapper.Map<MemberDto>(member);

            Assert.AreEqual("email2", memebrDto.Email);
            
          

          
            Ydb.Finance.DomainModel.BalanceTotal total = new Finance.DomainModel.BalanceTotal { Total = 15 };
            Ydb.Finance.Application.BalanceTotalDto bdto = Mapper.Map<Ydb.Finance.Application.BalanceTotalDto>(total);

            Assert.AreEqual(15, bdto.Total);






        }
    }
}
