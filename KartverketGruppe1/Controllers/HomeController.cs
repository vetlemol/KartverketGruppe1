using KartverketGruppe1.Services;
using KartverketGruppe1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using KartverketGruppe1.Data;

namespace KartverketGruppe1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IKommuneInfoService _kommuneInfoService;
        private readonly IStedsnavnService _stedsnavnService;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IKommuneInfoService kommuneInfoService, IStedsnavnService stedsnavnService, ApplicationDbContext context)
        {
            _logger = logger;
            _kommuneInfoService = kommuneInfoService;
            _stedsnavnService = stedsnavnService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LagBruker()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BrukerProfil()
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

        // Tom Liste for Stedsnavn for å kunne søke etter Stedsnavn i kartavvik uten error ved første visning
        public IActionResult KartInnmelding()
        {
            return View(new List<StedsnavnViewModel>());
        }


        // Håndterer søk etter Stedsnavn i kartinnmelding
        // Funker, ikke rør :)
        [HttpPost]
        public async Task<IActionResult> SokStedsnavn(string? SokeTekst)
        {
            if (string.IsNullOrEmpty(SokeTekst))
            {
                return View("KartInnmelding");
            }

            // Får fortsatt ArgumentNullException hvis den ikke finner noe på søketekst

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
        
        
       public IActionResult Hjelp()
    {
        return View();
    }

            
        
        // H�ndterer s�k etter Kommuneinformasjon

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

        // View for sÃ¸k etter Stedsnavn og kommuneinformasjon
        public IActionResult Sok()
        {
            return View();
        }

        // Handterer sok etter Stedsnavn
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





        [HttpGet]
        public IActionResult TestVetle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TestVetle(string Fornavn, string Etternavn, string Epost, string Ansvarsområde, string Avdeling, string Passord)
        {
            try
            {
                if (string.IsNullOrEmpty(Fornavn) || string.IsNullOrEmpty(Etternavn) || string.IsNullOrEmpty(Epost) || string.IsNullOrEmpty(Ansvarsområde) || string.IsNullOrEmpty(Passord))
                {
                    ViewData["Error"] = "Please fill out all fields.";
                    return View();
                }

                var nysaksbehandler = new Saksbehandler
                {
                    Fornavn = Fornavn,
                    Etternavn = Etternavn,
                    Epost = Epost,
                    Ansvarsområde = Ansvarsområde,
                    Avdeling = Avdeling,
                    Passord = Passord
                };

                _context.Saksbehandler.Add(nysaksbehandler);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View();
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