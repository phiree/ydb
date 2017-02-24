using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Common.Domain;

public class MockData
{
    /// <summary>
    /// 当前代理
    /// </summary>
    public static MemberDto memberAgent = new MemberDto();

    /// <summary>
    /// 区域列表
    /// </summary>
    public static IList<Area> areaList = new List<Area> {
        new Area { Id = 2445, Name="海南省海口市"},
        new Area { Id = 2446, Name="海南省海口市市辖区"},
        new Area { Id = 2447, Name="海南省海口市秀英区"},
        new Area { Id = 2448, Name="海南省海口市龙华区"},
        new Area { Id = 2449, Name="海南省海口市琼山区"},
        new Area { Id = 2450, Name="海南省海口市美兰区"} };

    /// <summary>
    /// 区域Id列表
    /// </summary>
    public static IList<string> areaIdList = areaList.Select(x => x.Id.ToString()).ToList();

    /// <summary>
    /// 
    /// </summary>
    public static IList<DZMembershipCustomerServiceDto> memberNotVerifiedList
    {
        get
        {
            IList<DZMembershipCustomerServiceDto> memberList = new List<DZMembershipCustomerServiceDto>();
            for (int i = 0; i < 15; i++)
            {
                DZMembershipCustomerServiceDto member = new DZMembershipCustomerServiceDto()
                {
                    Id = Guid.NewGuid(),
                    DisplayName = "NotVerified" + i.ToString(),
                    UserCity = areaList[i % 6].Name,
                    Phone = "1363769130" + i.ToString(),
                    QQNumber = "50264711" + i.ToString(),
                    TimeCreated = DateTime.Now.AddDays(-12).AddHours(i),
                    ApplyTime = DateTime.Now.AddDays(-1).AddHours(i),
                    IsVerified = false,
                    RealName= "RealName" + i.ToString(),
                    PersonalID= "PersonalID" + i.ToString(),
                };
                memberList.Add(member);
            }
            return memberList;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static IList<DZMembershipCustomerServiceDto> memberAgreeVerifiedList
    {
        get
        {
            IList<DZMembershipCustomerServiceDto> memberList = new List<DZMembershipCustomerServiceDto>();
            for (int i = 0; i < 15; i++)
            {
                DZMembershipCustomerServiceDto member = new DZMembershipCustomerServiceDto()
                {
                    Id = Guid.NewGuid(),
                    DisplayName = "AgreeVerified" + i.ToString(),
                    UserCity = areaList[i % 6].Name,
                    Phone = "1363769130" + i.ToString(),
                    QQNumber = "50264711" + i.ToString(),
                    TimeCreated = DateTime.Now.AddDays(-12).AddHours(i),
                    ApplyTime = DateTime.Now.AddDays(-1).AddHours(i),
                    IsVerified = true,
                    VerificationIsAgree = true,
                    RealName = "RealName" + i.ToString(),
                    PersonalID = "PersonalID" + i.ToString(),
                };
                memberList.Add(member);
            }
            return memberList;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static IList<DZMembershipCustomerServiceDto> memberRefuseVerifiedList
    {
        get
        {
            IList<DZMembershipCustomerServiceDto> memberList = new List<DZMembershipCustomerServiceDto>();
            for (int i = 0; i < 15; i++)
            {
                DZMembershipCustomerServiceDto member = new DZMembershipCustomerServiceDto()
                {
                    Id = Guid.NewGuid(),
                    DisplayName = "RefuseVerified" + i.ToString(),
                    UserCity = areaList[i % 6].Name,
                    Phone = "1363769130" + i.ToString(),
                    QQNumber = "50264711" + i.ToString(),
                    TimeCreated = DateTime.Now.AddDays(-12).AddHours(i),
                    ApplyTime = DateTime.Now.AddDays(-1).AddHours(i),
                    IsVerified = true,
                    VerificationIsAgree = false,
                    RealName = "RealName" + i.ToString(),
                    PersonalID = "PersonalID" + i.ToString(),
                };
                memberList.Add(member);
            }
            return memberList;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static IList<DZMembershipCustomerServiceDto> memberMyList
    {
        get
        {
            IList<DZMembershipCustomerServiceDto> memberList = new List<DZMembershipCustomerServiceDto>();
            for (int i = 1; i < 15; i++)
            {
                DZMembershipCustomerServiceDto member = new DZMembershipCustomerServiceDto()
                {
                    Id = Guid.NewGuid(),
                    DisplayName = "My" + i.ToString(),
                    UserCity = areaList[i % 6].Name,
                    Phone = "1363769130" + i.ToString(),
                    QQNumber = "50264711" + i.ToString(),
                    TimeCreated = DateTime.Now.AddDays(-12).AddHours(i),
                    ApplyTime = DateTime.Now.AddDays(-1).AddHours(i),
                    IsVerified = true,
                    VerificationIsAgree = true,
                    RealName = "RealName" + i.ToString(),
                    PersonalID = "PersonalID" + i.ToString(),
                };
                memberList.Add(member);
            }
            return memberList;
        }
    }

    /// <summary>
    /// assistant_validate
    /// </summary>
    public static IDictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dicDto
    {
        get
        {
            IDictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dic = new Dictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>>();
            dic[Enum_ValiedateCustomerServiceType.NotVerifiedCustomerService] = memberNotVerifiedList;
            dic[Enum_ValiedateCustomerServiceType.AgreeVerifiedCustomerService] = memberAgreeVerifiedList;
            dic[Enum_ValiedateCustomerServiceType.RefuseVerifiedCustomerService] = memberRefuseVerifiedList;
            dic[Enum_ValiedateCustomerServiceType.MyCustomerService] = memberMyList;
            return dic;
        }
    }

    /// <summary>
    /// assistant_validate_info
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static DZMembershipCustomerServiceDto GetDZMembershipCustomerServiceDtoById(string id, string type)
    {
        Enum_ValiedateCustomerServiceType t = (Enum_ValiedateCustomerServiceType)Enum.Parse(typeof(Enum_ValiedateCustomerServiceType), type);
        DZMembershipCustomerServiceDto member = dicDto[t].FirstOrDefault(x => x.Id == new Guid(id));
        return member;
    }
}