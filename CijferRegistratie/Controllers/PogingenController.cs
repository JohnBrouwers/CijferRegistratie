using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CijferRegistratie.Data;
using CijferRegistratie.Data.Entities;
using CijferRegistratie.Models;

namespace CijferRegistratie.Controllers
{
    public class PogingController : Controller
    {
        private readonly CijferRegistratieDbContext _context;

        public PogingController(CijferRegistratieDbContext context)
        {
            _context = context;
        }

        // GET: Pogingen
        public async Task<IActionResult> Index()
        {
            var cijferRegistratieDbContext = _context.Pogingen.Include(p => p.Vak);
            return View(await cijferRegistratieDbContext.ToListAsync());
        }

        // GET: Pogingen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pogingen == null)
            {
                return NotFound();
            }

            var poging = await _context.Pogingen
                .Include(p => p.Vak)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poging == null)
            {
                return NotFound();
            }

            return View(poging);
        }

        #region Create
        // GET: Pogingen/Create
        public IActionResult Create()
        {
            ViewData["Pogingen"] = new SelectList(_context.Vakken, "Id", "Naam");
            return View();
        }

        // POST: Pogingen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Jaar,Resultaat,VakId")] Poging poging)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poging);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VakPogingen"] = new SelectList(_context.Vakken, "Id", "Naam", poging.VakId);
            return View(poging);
        } 
        #endregion

        #region CreateSpecial

        // GET: Pogingen/Create
        public async Task<IActionResult> CreateSpecial(int vakId = 0)
        {
            var model = await _context.Vakken.Where(v => v.Id == vakId).Select(v => new PogingCreateViewModel(vakId, v.Naam)).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Pogingen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSpecial([Bind("VakId,Jaar,Resultaat")] PogingCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var nieuwePoging = new Poging { Jaar = model.Jaar, Resultaat = model.Resultaat, VakId = model.VakId };
                _context.Add(nieuwePoging);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(model);
        }
        #endregion

        // GET: Pogingen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pogingen == null)
            {
                return NotFound();
            }

            var poging = await _context.Pogingen.FindAsync(id);
            if (poging == null)
            {
                return NotFound();
            }
            ViewData["VakId"] = new SelectList(_context.Vakken, "Id", "Naam", poging.VakId);
            return View(poging);
        }

        // POST: Pogingen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Jaar,Resultaat,VakId")] Poging poging)
        {
            if (id != poging.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poging);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PogingExists(poging.Id))
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
            ViewData["VakId"] = new SelectList(_context.Vakken, "Id", "Naam", poging.VakId);
            return View(poging);
        }

        // GET: Pogingen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pogingen == null)
            {
                return NotFound();
            }

            var poging = await _context.Pogingen
                .Include(p => p.Vak)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poging == null)
            {
                return NotFound();
            }

            return View(poging);
        }

        // POST: Pogingen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pogingen == null)
            {
                return Problem("Entity set 'CijferRegistratieDbContext.Pogingen'  is null.");
            }
            var poging = await _context.Pogingen.FindAsync(id);
            if (poging != null)
            {
                _context.Pogingen.Remove(poging);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PogingExists(int id)
        {
          return (_context.Pogingen?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
