using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using outdoor.rocks.App_Start;
using outdoor.rocks.Providers;

namespace outdoor.rocks
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }
                
        public void ConfigureAuth(IAppBuilder app)
        {                 
            app.CreatePerOwinContext(ApplicationIdentityContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
                        
            //OAuth settings with bearer token
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                
                // В рабочем режиме задайте AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            //Implements tokens
            app.UseOAuthBearerTokens(OAuthOptions);

        }
    }
}
