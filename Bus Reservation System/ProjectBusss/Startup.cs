using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectBusss.Startup))]
namespace ProjectBusss
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
