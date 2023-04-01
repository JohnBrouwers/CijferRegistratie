using CijferRegistratie.Data;
using CijferRegistratie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;//move away from Newtonsoft
using System.Diagnostics;
using System.Net.Http;

namespace CijferRegistratie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CijferRegistratieDbContext _context;

        public HomeController(ILogger<HomeController> logger, 
            CijferRegistratieDbContext context,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vakListItems = await _context.Vakken.OrderByDescending(v => v.EC)
                .Select(v => new VakListItemViewModel(
                    v.Id,
                    v.Naam,
                    v.EC,
                    v.Pogingen.Count,
                    v.Pogingen.Count > 0 ? v.Pogingen.Max(p => p.Resultaat) : null
                //v.Pogingen.Select(p => p.Resultaat).OrderByDescending(p => p).FirstOrDefault(-1)
                )).ToListAsync();

            var model = new VakkenListViewModel();
            model.VakListItems = vakListItems;

            string jsonMutaties = this.HttpContext.Session.GetString("Mutaties") ?? "[]";
            string[] mutaties = JsonSerializer.Deserialize<string[]>(jsonMutaties) ?? Array.Empty<string>();
            model.Mutaties = mutaties;

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