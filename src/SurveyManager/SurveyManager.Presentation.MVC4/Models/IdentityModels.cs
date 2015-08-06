using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace SurveyManager.Presentation.MVC4.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name)
            : base(name)
        { }
    }

    public class ApplicationUserRole : IdentityUserRole
    {
        public ApplicationUserRole()
            : base()
        { }

        public ApplicationRole Role { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ProfileManagerConnString", throwIfV1Schema: false)
        {
            Database.SetInitializer(new IdentityDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override IDbSet<ApplicationUser> Users { get; set; }
    }

    public class IdentityDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            SeedIdentity(context);
            base.Seed(context);
        }
      
        public static void SeedIdentity(ApplicationDbContext db)
        {
            var userManager =
                HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            var roleManager =
                HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string name = "admin@infnet.edu.br";
            const string password = "Admin@123";
            List<string> roleNames = new List<string>() {"Administrador", "Aluno", "Professor"};

            //Create Role Admin if it does not exist
            foreach (string roleName in roleNames)
            {
                var role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    var roleresult = roleManager.Create(role);
                }
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            var adminRole = roleManager.FindByName(roleNames[0]);
            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(adminRole.Name))
            {
                var result = userManager.AddToRole(user.Id, adminRole.Name);
            }
        }
    }
}