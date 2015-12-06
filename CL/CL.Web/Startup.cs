using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CL.Web.Startup))]
namespace CL.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
