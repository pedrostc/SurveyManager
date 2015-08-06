using Microsoft.Owin;
using Owin;
using SurveyManager.Presentation.MVC4;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace SurveyManager.Presentation.MVC4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
