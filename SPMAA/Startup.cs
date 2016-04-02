using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SPMAA.Startup))]
namespace SPMAA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
