using KartverketGruppe1.Services;
using KartverketGruppe1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KartverketGruppe1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IKommuneInfoService _kommuneInfoService;
        private readonly IStedsnavnService _stedsnavnService;

        public HomeController(ILogger<HomeController> logger, IKommuneInfoService kommuneInfoService, IStedsnavnService stedsnavnService)
        {
            _logger = logger;
            _kommuneInfoService = kommuneInfoService;
            _stedsnavnService = stedsnavnService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LagBruker()
        {
            return View();
        }

        public IActionResult Start()
        {
            return View();
        }

        public IActionResult Innlogging()
        {
            return View();
        }

        // Tom Liste for Stedsnavn for � kunne s�ke etter Stedsnavn i kartavvik uten error ved f�rste visning
        public IActionResult KartInnmelding()
        {
            return View(new List<StedsnavnViewModel>());
        }


        // H�ndterer s�k etter Stedsnavn i kartinnmelding
        // Funker, ikke r�r :)
        [HttpPost]
        public async Task<IActionResult> SokStedsnavn(string? SokeTekst)
        {
            if (string.IsNullOrEmpty(SokeTekst))
            {
                return View("KartInnmelding");
            }

            // F�r fortsatt ArgumentNullException hvis den ikke finner noe p� s�ketekst

            var stedsnavnResponse = await _stedsnavnService.GetStedsnavnAsync(SokeTekst);
            if (stedsnavnResponse?.Navn != null && stedsnavnResponse.Navn.Any())
            {
                var viewModel = stedsnavnResponse.Navn.Select(n => new StedsnavnViewModel
                {
                    Nord = n.Representasjonspunkt.Nord,
                    Ost = n.Representasjonspunkt.Ost
                }).ToList();

                return View("KartInnmelding", viewModel);
            }
            else
            {
                ViewData["Error"] = $"No results found for '{SokeTekst}'.";
                return View("KartInnmelding");
            }
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        // Håndterer søk etter Kommuneinformasjon
        [HttpPost]
        public async Task<IActionResult> KommuneInfo(string kommuneNr)
        {
            if (string.IsNullOrEmpty(kommuneNr))
            {
                ViewData["Error"] = "Please enter a valid Kommune Number.";
                return View("Index");
            }

            var kommuneInfo = await _kommuneInfoService.GetKommuneInfoAsync(kommuneNr);
            if (kommuneInfo != null)
            {
                var viewModel = new KommuneInfoViewModel
                {
                    Kommunenavn = kommuneInfo.Kommunenavn,
                    Kommunenummer = kommuneInfo.Kommunenummer,
                    Fylkesnavn = kommuneInfo.Fylkesnavn,
                    SamiskForvaltningsomrade = kommuneInfo.SamiskForvaltningsomrade
                };
                return View("KommuneInfo", viewModel);
            }
            else
            {
                ViewData["Error"] = $"No results found for Kommune Number '{kommuneNr}'.";
                return View("Index");
            }
        }

        // View for søk etter Stedsnavn og kommuneinformasjon
        public IActionResult Sok()
        {
            return View();
        }
        // håndterer søk etter Stedsnavn
        [HttpPost]
        public async Task<IActionResult> Stedsnavn(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                ViewData["Error"] = "Please enter a valid place name.";
                return View("Index");
            }

            var stedsnavnResponse = await _stedsnavnService.GetStedsnavnAsync(searchTerm);
            if (stedsnavnResponse?.Navn != null && stedsnavnResponse.Navn.Any())
            {
                var viewModel = stedsnavnResponse.Navn.Select(n => new StedsnavnViewModel
                {
                    Skrivemate = n.Skrivemate,
                    Navneobjekttype = n.Navneobjekttype,
                    Sprak = n.Sprak,
                    Navnestatus = n.Navnestatus,
                    Nord = n.Representasjonspunkt.Nord,
                    Ost = n.Representasjonspunkt.Ost
                }).ToList();

                return View("Stedsnavn", viewModel);
            }
            else
            {
                ViewData["Error"] = $"No results found for '{searchTerm}'.";
                return View("Index");
            }
        }






        // Laster inn tilfeldig bakgrunnsbilde fra wwwroot/Bakgrunnsbilder
        public IActionResult GetRandomBackgroundImage()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Bakgrunnsbilder");
            var files = Directory.GetFiles(path, "*.png").Select(Path.GetFileName).ToList();

            if (files.Count == 0)
            {
                return Json(new { error = "Ingen bilder funnet" });
            }

            Random rnd = new Random();
            string randomImage = files[rnd.Next(files.Count)];

            return Json(new { imagePath = $"/Bakgrunnsbilder/{randomImage}" });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
