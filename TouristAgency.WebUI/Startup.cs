using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TouristAgency.WebUI.Startup))]
namespace TouristAgency.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
