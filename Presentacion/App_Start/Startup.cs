using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Presentacion
{
    public static class MyAuthentication
    {
        public const String ApplicationCookie = "ADAuthCookie";
    }

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // need to add UserManager into owin, because this is used in cookie invalidation
            if (System.Diagnostics.Debugger.IsAttached)
            {
                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = MyAuthentication.ApplicationCookie,
                    LoginPath = new PathString("/Home/Index"),
                    Provider = new CookieAuthenticationProvider(),
                    CookieName = "LoginCookie",
                    CookieHttpOnly = true,
                    ExpireTimeSpan = TimeSpan.FromHours(12), // adjust to your needs
                });
            }
            else
            {
                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = MyAuthentication.ApplicationCookie,
                    LoginPath = new PathString("/Login/Login"),
                    Provider = new CookieAuthenticationProvider(),
                    CookieName = "LoginCookie",
                    CookieHttpOnly = true,
                    ExpireTimeSpan = TimeSpan.FromHours(12), // adjust to your needs
                });
            }
            
        }
    }
}