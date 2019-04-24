using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BooKX.Startup))]
namespace BooKX
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
