using Hospital.Model;
using Hospital.Repositery;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hopital.Uitility
{
    public class DefaultUserIntializer : IDefaultUserIntializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DefaultUserIntializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (!_roleManager.RoleExistsAsync(WebsiteRoles.WebSite_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebSite_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebSite_Patient)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebSite_Doctor)).GetAwaiter().GetResult();

                _userManager.CreateAsync(
                    new ApplicationUser
                    {
                        UserName = "Nikhil",
                        Email = "nik@gmail.com"
                    }, "Nikhil@123"
                ).GetAwaiter().GetResult();

                var appUser = _context.ApplicationUser.FirstOrDefault(x => x.Email.ToLower() == "nik@gmail.com");
                if (appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser, WebsiteRoles.WebSite_Admin).GetAwaiter().GetResult();
                }
            }
        }
    }
} 
