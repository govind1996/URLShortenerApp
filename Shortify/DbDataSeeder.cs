using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shortify
{
    public class DbDataSeeder
    {
        private readonly UrlShortnerDbContext _context;
        public DbDataSeeder(UrlShortnerDbContext context)
        {
            _context = context;
        }
        public async void SeedAdminUser()
        {

            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "Admin"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "1" });
            }
            if (!_context.Roles.Any(r => r.Name == "User"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "User", NormalizedName = "USER", ConcurrencyStamp = "2" });
            }
            //if (!_context.Roles.Any(r => r.Name == "admin"))
            //{
            //    await roleStore.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "ADMIN", ConcurrencyStamp:1 });
            //}
            //if (!_context.Users.Any(u => u.UserName == user.UserName))
            //{
            //    var password = new PasswordHasher<ApplicationUser>();
            //    var hashed = password.HashPassword(user, "password");
            //    user.PasswordHash = hashed;
            //    var userStore = new UserStore<ApplicationUser>(_context);
            //    await userStore.CreateAsync(user);
            //    await userStore.AddToRoleAsync(user, "admin");
            //}

            await _context.SaveChangesAsync();
        }
    }
}

