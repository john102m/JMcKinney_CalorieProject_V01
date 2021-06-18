using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalorieProject_V01.Startup))]
namespace CalorieProject_V01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
