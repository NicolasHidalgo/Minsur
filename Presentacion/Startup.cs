using Microsoft.Owin;
using Owin;
using System.Configuration;
using System.Web.Configuration;

[assembly: OwinStartupAttribute(typeof(Presentacion.Startup))]
namespace Presentacion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AuthenticationMode authMode = ((AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication")).Mode;
            var compilation = (CompilationSection)ConfigurationManager.GetSection("system.web/compilation");
            var server = System.Environment.MachineName;

            if (authMode.ToString() == "Forms")
            {
                ConfigureAuth(app);
            }
            
        }
    }
}