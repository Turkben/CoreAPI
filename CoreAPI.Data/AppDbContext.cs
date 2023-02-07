using CoreAPI.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //configuration entity flueantapi
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            //Seeding a  'admin' role to AspNetRoles table
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                Name = "Admin",
                NormalizedName = "ADMIN".ToUpper()
            });

            //builder.Entity<IdentityRole>().HasData(new IdentityRole
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "Manager",
            //    NormalizedName = "MANAGER".ToUpper()
            //});

            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();


            //Seeding the User to AspNetUsers table
            builder.Entity<User>().HasData(
                new User
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    UserName = "gokhan",
                    Email ="g.turkben@gmail.com",
                    NormalizedEmail= "G.TURKBEN@GMAIL.COM",
                    NormalizedUserName = "GOKHAN",
                    City = "Ankara",
                    PasswordHash = hasher.HashPassword(null, "Sifre123!")
                }
            );

            //Seeding the relation between our user and role to AspNetUserRoles table
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );

            base.OnModelCreating(builder);
        }

    }
}
