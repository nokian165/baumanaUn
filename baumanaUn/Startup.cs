using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(baumanaUn.Startup))]
namespace baumanaUn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
