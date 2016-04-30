﻿using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Api.Startup))]
namespace Api
{
    
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}
