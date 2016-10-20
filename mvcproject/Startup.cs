using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvcproject.Startup))]
namespace mvcproject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
