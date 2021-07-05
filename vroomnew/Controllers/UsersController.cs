using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vroomnew.DbContext;
using vroomnew.Models;

namespace vroomnew.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext _db;
        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Users()
        {
            return View((IEnumerable<ApplicationUser>)_db.Users.ToList());
        }
        [HttpPost]
        public IActionResult Delete(string id)
        {
            var user = _db.Users.Find(id);
            if (user == null) return NotFound();
            _db.Users.Remove(user);
            _db.SaveChanges();
            return RedirectToAction("Users");

        }
    }
}
