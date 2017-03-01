using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AdminAgent.Controllers
{
    public abstract class AgentBaseController: Controller
    {
        protected AppUser CurrentUser => new AppUser(this.User as ClaimsPrincipal);
    }

    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public Guid UserId => new Guid(this.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}