using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FunTranslator2.Startup))]
namespace FunTranslator2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
