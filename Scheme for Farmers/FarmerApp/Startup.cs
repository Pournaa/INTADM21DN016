using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FarmerApp.Startup))]
namespace FarmerApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
