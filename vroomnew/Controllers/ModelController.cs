using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vroomnew.Controllers.Resources;
using vroomnew.DbContext;
using vroomnew.Models;

namespace vroomnew.Controllers
{
    public class ModelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ModelController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Model
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Models.Include(m => m.Make);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Model/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .Include(m => m.Make)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Model/Create
        public IActionResult Create()
        {
            ViewData["makeId"] = new SelectList(_context.Makes, "Id", "Name");
            return View();
        }

        // POST: Model/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,makeId")] Model model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["makeId"] = new SelectList(_context.Makes, "Id", "Name", model.makeId);
            return View(model);
        }

        // GET: Model/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewData["makeId"] = new SelectList(_context.Makes, "Id", "Name", model.makeId);
            return View(model);
        }

        // POST: Model/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,makeId")] Model model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["makeId"] = new SelectList(_context.Makes, "Id", "Name", model.makeId);
            return View(model);
        }

        // GET: Model/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .Include(m => m.Make)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Model/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Models.FindAsync(id);
            _context.Models.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelExists(int id)
        {
            return _context.Models.Any(e => e.Id == id);
        }

        [AllowAnonymous]
        [HttpGet("api/models")]
        public IEnumerable<ModelResources> Models()
        {
            var models = _context.Models.Include(x => x.Make).ToList();
              
            return _mapper.Map<List<Model>, List<ModelResources>>(models);
        }
        [AllowAnonymous]
        [HttpGet("api/models/{MakeID}")]
        public IEnumerable<ModelResources> Models(int MakeID)
        {
            var models= _context.Models.Include(x=>x.Make).ToList(). Where(x=>x.makeId==MakeID).ToList();
           // var config = new MapperConfiguration(cm => cm.CreateMap<Model, ModelResources>());
           // var mapper = new Mapper(config);
            var modelResources = _mapper.Map<List<Model>, List<ModelResources>>(models);

            //var modelResources = models.Select(m => new ModelResources
            //{
            //    Id = m.Id,
            //    Name = m.Name
            //}).ToList();
            return modelResources;
        }
    }
}
