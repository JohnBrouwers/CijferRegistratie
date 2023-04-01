using CijferRegistratie.Data;
using CijferRegistratie.Data.Entities;
using CijferRegistratie.Models;
using CijferRegistratie.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CijferRegistratie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CijferRegistratieDbContext _context;

        public HomeController(ILogger<HomeController> logger, 
            CijferRegistratieDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            #region Sample_SessionExtensionMethod

            //ieder type met een default constructor kun je met deze extension method opslaan
            var gemNotInSession = this.HttpContext.Session.Get<Vak>("gem");
            this.HttpContext.Session.Set<Vak>("gem", new Vak { Naam = "Generic Extension Methods" });
            var gemFromSession = this.HttpContext.Session.Get<Vak>("gem");

            #endregion

            var vakListItems = await _context.Vakken.OrderByDescending(v => v.EC)
                .Select(v => new VakListItemViewModel(
                    v.Id,
                    v.Naam,
                    v.EC,
                    v.Pogingen.Count,
                    v.Pogingen.Count > 0 ? v.Pogingen.Max(p => p.Resultaat) : null
                )).ToListAsync();

            var model = new VakkenListViewModel();
            model.VakListItems = vakListItems;
            model.Mutaties = this.HttpContext.Session.Get<List<string>>("Mutaties").ToArray() ;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}