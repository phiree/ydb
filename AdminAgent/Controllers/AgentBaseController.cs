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
    }

    public class AppUser : ClaimsPrincipal
    {
        int _UserAreaId;
        Area _UserArea;
        IList<Area> _AreaList;
        IList<string> _AreaIdList;
        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        {
            IAreaService areaService = Bootstrap.Container.Resolve<IAreaService>();
            _UserAreaId= int.Parse(this.FindFirst(ClaimTypes.StateOrProvince).Value ?? "0");
            _UserArea = areaService.GetOne(_UserAreaId);
            _AreaList = _UserArea == null ? new List<Area>() : areaService.GetSubArea(_UserArea.Code);
            _AreaIdList = _AreaList.Select(x=>x.Id.ToString ()).ToList();
        }
        public Guid UserId => new Guid(this.FindFirst(ClaimTypes.NameIdentifier).Value);
        public string UserEmail => this.FindFirst(ClaimTypes.Email).Value;
        public string UserName => this.FindFirst(ClaimTypes.Name).Value;
        public string UserPhone => this.FindFirst(ClaimTypes.MobilePhone).Value;
        public int UserAreaId => _UserAreaId;
        public Area UserArea => _UserArea;
        public IList<Area> AreaList => _AreaList;
        public IList<string> AreaIdList => _AreaIdList;
    }

}