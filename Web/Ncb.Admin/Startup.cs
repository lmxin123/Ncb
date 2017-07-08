using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Framework.Auth;

[assembly: OwinStartup(typeof(Ncb.Admin.Startup))]

namespace Ncb.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            StartupAuth auth = new StartupAuth();
            auth.ConfigureAuth(app);
        }
    }
}
