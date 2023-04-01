using CijferRegistratie.Data;
using CijferRegistratie.Data.Entities;
using CijferRegistratie.Models;
using CijferRegistratie.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CijferRegistratie.Controllers
{
    public class PogingController : Controller
    {
        private readonly CijferRegistratieDbContext _context;
        private readonly HttpClient _httpClient;
        public PogingController(CijferRegistratieDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
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
        public async Task<IActionResult> CreateSpecial([Bind("VakId,Vak,Jaar,Resultaat")] PogingCreateModel model)
        {
            if (ModelState.IsValid)
            {
                string studentType = await GetStudentTypeFromApiAsync();

                var nieuwePoging = new Poging { Jaar = model.Jaar, Resultaat = model.Resultaat, VakId = model.VakId, StudentType = studentType };
                _context.Add(nieuwePoging);
                await _context.SaveChangesAsync();

                List<string> mutaties = this.HttpContext.Session.Get<List<string>>("Mutaties");
                mutaties.Add($"Poging toegevoegd aan: {model.Vak}, met resultaat: {model.Resultaat} ({studentType})");
                if (model.Resultaat >= 6) {                 
                    mutaties.Add($"Vak {model.Vak} is behaald");
                }
                this.HttpContext.Session.Set<List<string>>("Mutaties", mutaties);

                return RedirectToAction(nameof(Index), "Home");
            }
            return View(model);
        }

        private async Task<string> GetStudentTypeFromApiAsync()
        {
            string? studentType = string.Empty;
            try
            {
                var result = await _httpClient.GetAsync("https://localhost:7107/studenttype");
                studentType = await result.Content.ReadAsStringAsync();
            }
            catch
            {
                Console.WriteLine("OOPS.. do some logging here..");
            }
            return studentType;
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
