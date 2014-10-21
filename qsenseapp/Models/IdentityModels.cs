using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace qsenseapp.Models
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

            //stuff please publish

            //is this working down
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
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

        public System.Data.Entity.DbSet<BusinessPartner> BusinessPartners { get; set; }
        public System.Data.Entity.DbSet<Job> Jobs { get; set; }
        public System.Data.Entity.DbSet<qsenseapp.Models.JobExpense> JobExpenses { get; set; }
        public System.Data.Entity.DbSet<qsenseapp.Models.Activity> Activities { get; set; }

    }
}