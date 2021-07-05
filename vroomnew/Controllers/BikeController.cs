using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vroomnew.DbContext;
using vroomnew.Models;
using vroomnew.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;

namespace vroomnew.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BikeController : Controller
    {
        [BindProperty]
        private BikeViewModel bikeModel { get; set; }
        private readonly ApplicationDbContext _db;
        [Obsolete]
        private readonly IHostingEnvironment   _hostingEnvironment;

        [Obsolete]
     
        public BikeController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            bikeModel = new BikeViewModel()
            {
                Makes = _db.Makes.ToList(),
                Models = _db.Models.ToList(),
                Bike = new Bike()
            };

        }
        //public IActionResult Index2()
        //{
        //    var bikes = _db.Bikes.Include(m=>m.Make).Include(x=>x.Model).ToList();
        //    return View(bikes);
        //}
        [AllowAnonymous]
        public IActionResult Index(string searchString, string sortOrder,int pageNumber=1,int pageSize=2)
        {
            ViewBag.CurrentSOrtOrder = sortOrder;
            ViewBag.CurrentsearchString = searchString;
            ViewBag.PriceSortParam =string.IsNullOrEmpty(sortOrder)? "price-desc":"";
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var bikes =from b in _db.Bikes.Include(m => m.Make).Include(x => x.Model) select b;
            var bikesCount = _db.Bikes.Count();
            if (!string.IsNullOrEmpty(searchString))
            {
                bikes = bikes.Where(b => b.Make.Name.ToLower().Contains(searchString.ToLower()));
                bikesCount = bikes.Count();
            }
            //Sorting logic
            switch (sortOrder)
            {
                case "price-desc":
                    bikes = bikes.OrderByDescending(b => b.Price);
                    break;
                default:
                    bikes = bikes.OrderBy(b => b.Price);
                    break;
            }


          bikes=bikes.Skip(ExcludeRecords).Take(pageSize);
            var result = new PagedResult<Bike>()
            {
                Data = bikes.AsNoTracking().ToList(),
                TotalItems = bikesCount,
                PageSize=pageSize,
                PageNumber=pageNumber

             };
            return View(result);
        }
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var bike = _db.Bikes.Include(r => r.Model).FirstOrDefault(s => s.Id == id);
            if (bike == null)
            {
                return NotFound();
            }

            return View(bike);
        }
        public IActionResult Create()
        {
           
            return View(bikeModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult CreatePost(BikeViewModel bikeModel )
        {
            if(!ModelState.IsValid)
            { return View(bikeModel); }
            _db.Bikes.Add(bikeModel.Bike);
            _db.SaveChanges();

            var bikeID = bikeModel.Bike.Id;
            var files = HttpContext.Request.Form.Files;
            UploadImage(files, bikeID);
          
            TempData["success"] = "Bike created successfully";
            return RedirectToAction("Index");                
        }
        public IActionResult Edit(int id)
        {
            bikeModel.Bike = _db.Bikes.Include(r => r.Model).FirstOrDefault(s => s.Id == id);
            bikeModel.Models = _db.Models.Where(m => m.makeId == bikeModel.Bike.MakeID);
            if (bikeModel.Bike == null)
            {
                return NotFound();
            }
          
            return View(bikeModel);
        }
        [HttpPost]
        [Obsolete]
        public IActionResult Edit(BikeViewModel bikeModel)
        {           
            if (!ModelState.IsValid)
            {
                return View(bikeModel);
            }
            var bikeID = bikeModel.Bike.Id;     
            _db.Entry(bikeModel.Bike).State = EntityState.Modified;
            //_db.Bikes.Update(bikeModel.Bike);
            _db.SaveChanges();
            var files = HttpContext.Request.Form.Files;
            if (files.Count() > 0)
            {
                                                
                UploadImage(files,bikeID);
            }
         
           
            TempData["success"] = "Bike updated successfully";
            return RedirectToAction("Index") ;
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var bike = _db.Bikes.Find(id);
            if (bike == null) return NotFound();
            _db.Bikes.Remove(bike);
            _db.SaveChanges();
            TempData["success"] = "Bike deleted successfully";
            return RedirectToAction("Index");
        }

        [Obsolete]
        public  void UploadImage(IFormFileCollection files,int bikeID)
        {
            string wwwrootPath = _hostingEnvironment.WebRootPath;
            var savedBike = _db.Bikes.Find(bikeID);
            if (files.Count() != 0)
            {
                var ImagePath = @"Images\bikes\";
                var Extension = Path.GetExtension(files[0].FileName);
                var relativeImagePath = ImagePath + Guid.NewGuid() + Extension;
                var AbsImagePath = Path.Combine(wwwrootPath, relativeImagePath);
                //upload file to server
                using (var fileStream = new FileStream(AbsImagePath, FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                savedBike.ImagePath = relativeImagePath;
                _db.Bikes.Update(savedBike);
                _db.SaveChanges();
            }

        }
    }
}
