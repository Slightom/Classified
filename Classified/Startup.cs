using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Classified.Startup))]
namespace Classified
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
