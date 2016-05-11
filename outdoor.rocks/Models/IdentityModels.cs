using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using AspNet.Identity.MongoDB;

namespace outdoor.rocks.Models
{
    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства 
    //в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ExternalBearer);
            
            // Add custom user claims here
            //Custom add types o Attribute Authorize
            //var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            //userIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            //userIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            //userIdentity.AddClaim(new Claim(ClaimTypes.Role, "User"));
           
            return userIdentity;
        }

    }
}
    