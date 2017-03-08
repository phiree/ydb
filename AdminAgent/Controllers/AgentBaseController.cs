using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Ydb.Common.Domain;
using Ydb.Common.Application;

namespace AdminAgent.Controllers
{
    public abstract class AgentBaseController: Controller
    {
        protected AppUser CurrentUser => new AppUser(this.User as ClaimsPrincipal);

        IAreaService areaService = Bootstrap.Container.Resolve<IAreaService>();
        protected Area UserArea => areaService.GetOne(CurrentUser.UserAreaId);
        protected IList<Area> AreaList => UserArea==null?new List<Area>():areaService.GetSubArea(UserArea.Code);
    }

    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }
        public Guid UserId => new Guid(this.FindFirst(ClaimTypes.NameIdentifier).Value);
        public string UserEmail => this.FindFirst(ClaimTypes.Email).Value;
        public string UserName => this.FindFirst(ClaimTypes.Name).Value;
        public string UserPhone => this.FindFirst(ClaimTypes.MobilePhone).Value;
        public int UserAreaId => int.Parse(this.FindFirst(ClaimTypes.StateOrProvince).Value??"0");
    }

}