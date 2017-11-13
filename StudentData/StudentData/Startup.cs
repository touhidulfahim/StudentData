using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentData.Startup))]
namespace StudentData
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
