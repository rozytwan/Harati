using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Harati.Startup))]
namespace Harati
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
