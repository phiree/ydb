using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminAgent.Startup))]
namespace AdminAgent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
