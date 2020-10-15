using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartupAttribute(typeof(Fapx_Web.Startup))]
namespace Fapx_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new DashboardOptions
            {
                AuthorizationFilters = new[]
{
                new LocalRequestsOnlyAuthorizationFilter()
            }
            };

            app.UseHangfireDashboard("/hangfire", options);
        }
    }
}