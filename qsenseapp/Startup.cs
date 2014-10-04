using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(qsenseapp.Startup))]
namespace qsenseapp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
