using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mikrotik.API.Startup))]
namespace Mikrotik.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
