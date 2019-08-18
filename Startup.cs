using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HTMLAPScraper.Startup))]
namespace HTMLAPScraper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
