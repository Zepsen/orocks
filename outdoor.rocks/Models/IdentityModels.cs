using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using AspNet.Identity.MongoDB;
using outdoor.rocks.App_Start;

namespace outdoor.rocks.Models
{   
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<ApplicationUser> manager)
        {            
            var userIdentity = await manager.CreateIdentityAsync(
                this, 
                DefaultAuthenticationTypes.ExternalBearer);

            
            return userIdentity;
        }
    }
}
    