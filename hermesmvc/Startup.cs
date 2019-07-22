using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(hermesmvc.Startup))]
namespace hermesmvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
