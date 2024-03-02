using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // seed Roles (User, Admin, SuperAdmin)

            var adminRoleID = "badf21be-95f4-4792-9616-023756943943";
            var superAdminRoleId = "8eacbf45-746e-4ed2-b6cb-f8ba6fbf37f9";
            var userRoleID = "e228c81e-2136-4f35-b500-50eb260f9271";
            var roles = new List<IdentityRole>
            {   
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id= adminRoleID,
                    ConcurrencyStamp=adminRoleID
                },
                 new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id= superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId
                },
                  new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id= userRoleID,
                    ConcurrencyStamp=userRoleID
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed SuperAdminUser
            var superAdminId = "90f51f16-b713-4a2d-a5e5-7073d75ae740";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id = superAdminId
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().
                HashPassword(superAdminUser, "Superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add All roles to SuperAdminUser
            var superAdminRoles= new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleID,
                    UserId = superAdminId,
                },
                new IdentityUserRole<string> 
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId,
                },
                new IdentityUserRole<string>
                {
                    RoleId= userRoleID,
                    UserId = superAdminId,
                }
            };  
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);  
        }
    }
}
