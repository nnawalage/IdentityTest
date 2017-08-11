using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityTest.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions() {
                AuthenticationType = "AppCookie",
                LoginPath = new PathString("/Authentication/login")
            });
        }
    }
}