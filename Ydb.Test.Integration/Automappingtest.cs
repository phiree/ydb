using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ydb.Membership.Application.Dto;
using Dianzhu.ApplicationService;
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
           
            Ydb.Membership.Application.AutoMapperConfiguration.Configure();
            Dianzhu.ApplicationService.Mapping.AutoMapperConfiguration.Configure();
            MemberDto member = Builder<MemberDto>.CreateNew().With(x=>x.Email="email2"). Build();
            customerObj    userobj = Mapper.Map<MemberDto, customerObj>(member);
            Assert.AreEqual("email2", userobj.email);


        }
    }
}
