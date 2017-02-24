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

    static IList<DZMembershipCustomerServiceDto> _memberNotVerifiedList;
    /// <summary>
    /// 
    /// </summary>
    public static IList<DZMembershipCustomerServiceDto> memberNotVerifiedList
    {
        get
        {
            if (_memberNotVerifiedList == null)
            {
                _memberNotVerifiedList = new List<DZMembershipCustomerServiceDto>();
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
                        RealName = "RealName" + i.ToString(),
                        PersonalID = "PersonalID" + i.ToString(),
                        DZMembershipImages = dzMembershipImageList,
                        DZMembershipDiploma=dzMembershipImageDiploma,
                        DZMembershipCertificates=dzMembershipImageCertificate,
                        Sex = i % 2 == 0
                    };
                    _memberNotVerifiedList.Add(member);
                }
            }
            return _memberNotVerifiedList;
        }
    }

    static IList<DZMembershipCustomerServiceDto> _memberAgreeVerifiedList;
    /// <summary>
    /// 
    /// </summary>
    public static IList<DZMembershipCustomerServiceDto> memberAgreeVerifiedList
    {
        get
        {
            if(_memberAgreeVerifiedList==null)
            {
                _memberAgreeVerifiedList = new List<DZMembershipCustomerServiceDto>();
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
                        DZMembershipImages = dzMembershipImageList,
                        DZMembershipDiploma = dzMembershipImageDiploma,
                        DZMembershipCertificates = dzMembershipImageCertificate,
                        Sex = i % 2 == 0
                    };
                    _memberAgreeVerifiedList.Add(member);
                }
            }
            return _memberAgreeVerifiedList;
        }
    }

    static IList<DZMembershipCustomerServiceDto> _memberRefuseVerifiedList;
    /// <summary>
    /// 
    /// </summary>
    public static IList<DZMembershipCustomerServiceDto> memberRefuseVerifiedList
    {
        get
        {
            if(_memberRefuseVerifiedList==null)
            {
                _memberRefuseVerifiedList = new List<DZMembershipCustomerServiceDto>();
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
                        DZMembershipImages = dzMembershipImageList,
                        DZMembershipDiploma = dzMembershipImageDiploma,
                        DZMembershipCertificates = dzMembershipImageCertificate,
                        Sex = i % 2 == 0
                    };
                    _memberRefuseVerifiedList.Add(member);
                }
            }
            return _memberRefuseVerifiedList;
        }
    }

    static IList<DZMembershipCustomerServiceDto> _memberMyList;
    /// <summary>
    /// 
    /// </summary>
    public static IList<DZMembershipCustomerServiceDto> memberMyList
    {
        get
        {
            if(_memberMyList==null)
            {
                _memberMyList = new List<DZMembershipCustomerServiceDto>();
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
                        DZMembershipImages = dzMembershipImageList,
                        DZMembershipDiploma = dzMembershipImageDiploma,
                        DZMembershipCertificates = dzMembershipImageCertificate,
                        Sex = i % 2 == 0
                    };
                    _memberMyList.Add(member);
                }
            }
            return _memberMyList;
        }
    }

    static IList<DZMembershipCustomerServiceDto> _memberLockedList;
    /// <summary>
    /// 
    /// </summary>
    public static IList<DZMembershipCustomerServiceDto> memberLockedList
    {
        get
        {
            if (_memberLockedList == null)
            {
                _memberLockedList = new List<DZMembershipCustomerServiceDto>();
                for (int i = 1; i < 15; i++)
                {
                    DZMembershipCustomerServiceDto member = new DZMembershipCustomerServiceDto()
                    {
                        Id = Guid.NewGuid(),
                        DisplayName = "Locked" + i.ToString(),
                        UserCity = areaList[i % 6].Name,
                        Phone = "1363769130" + i.ToString(),
                        QQNumber = "50264711" + i.ToString(),
                        TimeCreated = DateTime.Now.AddDays(-12).AddHours(i),
                        ApplyTime = DateTime.Now.AddDays(-1).AddHours(i),
                        IsVerified = true,
                        VerificationIsAgree = true,
                        RealName = "RealName" + i.ToString(),
                        PersonalID = "PersonalID" + i.ToString(),
                        DZMembershipImages = dzMembershipImageList,
                        DZMembershipDiploma = dzMembershipImageDiploma,
                        DZMembershipCertificates = dzMembershipImageCertificate,
                        Sex = i % 2 == 0
                    };
                    _memberLockedList.Add(member);
                }
            }
            return _memberLockedList;
        }
    }

    static IList<DZMembershipCustomerServiceDto> _memberUnLockedList;
    /// <summary>
    /// 
    /// </summary>
    public static IList<DZMembershipCustomerServiceDto> memberUnLockedList
    {
        get
        {
            if (_memberUnLockedList == null)
            {
                _memberUnLockedList = new List<DZMembershipCustomerServiceDto>();
                for (int i = 1; i < 15; i++)
                {
                    DZMembershipCustomerServiceDto member = new DZMembershipCustomerServiceDto()
                    {
                        Id = Guid.NewGuid(),
                        DisplayName = "UnLocked" + i.ToString(),
                        UserCity = areaList[i % 6].Name,
                        Phone = "1363769130" + i.ToString(),
                        QQNumber = "50264711" + i.ToString(),
                        TimeCreated = DateTime.Now.AddDays(-12).AddHours(i),
                        ApplyTime = DateTime.Now.AddDays(-1).AddHours(i),
                        IsVerified = true,
                        VerificationIsAgree = true,
                        RealName = "RealName" + i.ToString(),
                        PersonalID = "PersonalID" + i.ToString(),
                        DZMembershipImages = dzMembershipImageList,
                        DZMembershipDiploma = dzMembershipImageDiploma,
                        DZMembershipCertificates = dzMembershipImageCertificate,
                        Sex = i % 2 == 0
                    };
                    _memberUnLockedList.Add(member);
                }
            }
            return _memberUnLockedList;
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
    /// assistant_list
    /// </summary>
    public static IDictionary<Enum_LockCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dicLockDto
    {
        get
        {
            IDictionary<Enum_LockCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dic = new Dictionary<Enum_LockCustomerServiceType, IList<DZMembershipCustomerServiceDto>>();
            dic[Enum_LockCustomerServiceType.LockedCustomerService] = memberLockedList;
            dic[Enum_LockCustomerServiceType.UnLockedCustomerService] = memberUnLockedList;
            return dic;
        }
    }

    /// <summary>
    /// assistant_list
    /// </summary>
    public static decimal assistantPoint = 0.35m;

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

    public static DZMembershipImageDto dzMembershipImageDiploma = new DZMembershipImageDto { ImageName = "https://dev.ydban.cn/images/logo.png", ImageType= "Diploma" };

    public static IList<DZMembershipImageDto> dzMembershipImageCertificate = new List<DZMembershipImageDto>
    {
        new DZMembershipImageDto { ImageName = "http://dev.ydban.cn/agent_admin_preview/dist/libs/adminLTE/img/photo1.png", ImageType = "Certificate" },
        new DZMembershipImageDto { ImageName = "http://dev.ydban.cn/agent_admin_preview/dist/libs/adminLTE/img/photo2.png", ImageType = "Certificate" }
    };

    public static IList<DZMembershipImageDto> dzMembershipImageList = new List<DZMembershipImageDto>
    {
         new DZMembershipImageDto { ImageName = "https://dev.ydban.cn/images/logo.png" ,ImageType= "Diploma" },
    new DZMembershipImageDto { ImageName = "http://dev.ydban.cn/agent_admin_preview/dist/libs/adminLTE/img/photo1.png", ImageType = "Certificate" },
        new DZMembershipImageDto { ImageName = "http://dev.ydban.cn/agent_admin_preview/dist/libs/adminLTE/img/photo2.png", ImageType = "Certificate" }
    };
}