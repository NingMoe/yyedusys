using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace YY.Edu.Sys.Api.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加个人资料数据，若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。

    public class CustomIdentityUser : IdentityUser
    {
        public string NameIdentifier { get; set; }
    }
    public class ApplicationUser : CustomIdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            // todo OAuthDefaults.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity.AddClaim(new Claim("YY.Edu.Sys.Api.Models.RegisterViewModel.NameIdentifier", NameIdentifier));
            userIdentity.AddClaim(new Claim("YY.Edu.Sys.Api.Models.RegisterViewModel.Email", Email));

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

}