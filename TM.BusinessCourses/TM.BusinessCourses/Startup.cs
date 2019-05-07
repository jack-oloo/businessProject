using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TM.BusinessCourses.Startup))]
namespace TM.BusinessCourses
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
