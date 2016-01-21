using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrystalBallWebSite.Startup))]
namespace CrystalBallWebSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
