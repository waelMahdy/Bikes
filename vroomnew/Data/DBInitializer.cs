using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using vroomnew.DbContext;
using vroomnew.Helpers;
using vroomnew.Models;

namespace vroomnew.Data
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DBInitializer(ApplicationDbContext  db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public  void Initialize()
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();               
            }

            if (_db.Roles.Any(r => r.Name == Roles.Admin)) return;

                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();

            var user = new ApplicationUser { UserName = "Admin", Email = "Admin@yahoo.com", IsAdmin = true, LastName = "Mahdy", FirstName = "Wael" };
            _userManager.CreateAsync(user, "Lilo@2010").GetAwaiter().GetResult();


            _userManager.AddToRoleAsync(user, Roles.Admin).Wait();

        }
    }
}
