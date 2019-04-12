using DeepCart.DtProvider;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(DeepCart.Startup))]

namespace DeepCart
{
    // This tutorial is by DotNet Techy YouTube Channel
    // For more info about channel You can visit this link
    //  https://www.youtube.com/c/dotnettechy
    public class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {

            app.UseCors(CorsOptions.AllowAll);
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20),
                Provider = new DotNetTechyAuthServerProvider()

            };

            app.UseOAuthBearerTokens(OAuthOptions);
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

        }

        public void Configuration(IAppBuilder app)
        {
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
            GlobalConfiguration.Configure(WebApiConfig.Register);

        }

}
}