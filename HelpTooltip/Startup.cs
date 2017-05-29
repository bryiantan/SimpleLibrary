using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelpTooltip.Startup))]
namespace HelpTooltip
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
