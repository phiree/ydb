using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.Application.Dto;
namespace Ydb.Membership.Application
{
    /// <summary>
    /// 登录服务
    /// </summary>
   public interface ILoginService
    {
        ValidateResult Login(string username, string password);
    }
}
