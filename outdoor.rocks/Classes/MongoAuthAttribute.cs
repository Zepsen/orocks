using AspNet.Identity.MongoDB;
using outdoor.rocks.App_Start;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace outdoor.rocks.Classes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MongoAuthAttribute : AuthorizeAttribute
    {
        public MongoAuthAttribute()
        { }

        public override void OnAuthorization(HttpActionContext actionContext)
        {        
            base.OnAuthorization(actionContext);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var roleManager = ApplicationRoleManager.GetExist();
            var roles = roleManager.Roles;

            var role = HttpContext.Current.User.IsInRole("User");            
            return role;
        }


    }


}
