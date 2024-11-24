using KartverketGruppe1.Services;
using KartverketGruppe1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using KartverketGruppe1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KartverketGruppe1.Controllers
{
    [Authorize(Roles = "Bruker")] // Krever at brukeren er logget inn og har rollen "Bruker" for å ha tilgang til denne kontrolleren
    // [AllowAnonymous] blir brukt for å gi tilgang til noen funksjoner i kontrolleren uten å være logget inn for feks anonym innmelding
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IKommuneInfoService _kommuneInfoService;
        private readonly IStedsnavnService _stedsnavnService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IKommuneInfoService kommuneInfoService, IStedsnavnService stedsnavnService, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _kommuneInfoService = kommuneInfoService;
            _stedsnavnService = stedsnavnService;
            _context = context;
            _userManager = userManager;
        }

        // [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult InnmeldOversikt()
        //{
        //    return View();
        //}


        public async Task<IActionResult> InnmeldOversikt(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            var innmelding = await _context.Innmelding
                .Include(i => i.Koordinat)
                .Include(i => i.Bruker)
                .Include(i => i.Kommune)
                .Include(i => i.Avvikstype)
                .Include(i => i.Status)
                .Include(i => i.Prioritet)
                .Include(i => i.Saksbehandler)
                .FirstOrDefaultAsync(i => i.InnmeldingID == id &&
                    (i.BrukerID == currentUser.Id));

            if (innmelding == null)
            {
                return NotFound();
            }

            await LoadViewbags();
            return View(innmelding);
        }


        public IActionResult NyInnmelding() ////////////////////////////////////////////
        {
            return View();
        }


        private async Task LoadViewbags()
        {
            ViewBag.Avvikstype = _context.Avvikstype
                .Select(a => new SelectListItem
                {
                    Value = a.AvvikstypeID.ToString(),
                    Text = a.Type
                })
                .ToList();

            ViewBag.Status = _context.Status
                .Select(a => new SelectListItem
                {
                    Value = a.StatusID.ToString(),
                    Text = a.Statustype
                })
                .ToList();

            ViewBag.Prioritet = _context.Prioritet
                .Select(a => new SelectListItem
                {
                    Value = a.PrioritetID.ToString(),
                    Text = a.Prioritetsnivå
                })
                .ToList();
        }



            // Metoden viser en enkel oversikt over alle innmeldinger for en spesifikk bruker sortert etter dato
            public async Task<IActionResult> Oversikt()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var innmeldinger = await _context.Innmelding
                .Include(i => i.Status)
                .Include(i => i.Kommune)
                .Where(i => i.BrukerID == currentUser.Id)
                .OrderByDescending(i => i.Dato)
                .ToListAsync();

            return View(innmeldinger);
        }
    


        // Denne metoden viser detaljene for en spesifikk innmelding
        public async Task<IActionResult> InnmeldingOversikt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var innmelding = await _context.Innmelding
                .Include(i => i.Kommune)
                .Include(i => i.Status)
                .Include(i => i.Prioritet)
                .Include(i => i.Koordinat)
                .Include(i => i.Avvikstype)
                .Include(i => i.Bruker)
                .Include(i => i.Saksbehandler)
                .FirstOrDefaultAsync(m => m.InnmeldingID == id && m.BrukerID == currentUser.Id);

            if (innmelding == null)
            {
                return NotFound();
            }

            ViewBag.Statuser = await _context.Status.ToListAsync();
            ViewBag.Prioriteter = await _context.Prioritet.ToListAsync();

            return View(innmelding);
        }


        [HttpGet]
        public IActionResult LagBruker()
        {
            return View();
        }


        // Denne metoden viser detaljene for en statisk brukerprofil, og er kun for demonstrasjon
        private static BrukerProfilViewModel _brukerProfil = new BrukerProfilViewModel
            {
        //    Name = "Navn",
        //    Email = "Epostadresse",
        //    Phone = "********",
        //    Password = "Nytt Passord",
        //    GjentaPassword = "Gjenta Passord",
            SubmissionsPerMonth = new List<int> { 3, 2, 0, 3, 1, 0, 2, 1, 0, 4, 0, 0 },
            Years = new List<int> { 2022, 2023, 2024 }
            };

        [HttpGet]
        //public IActionResult BrukerProfil()
        //{
        //    return View(_brukerProfil);
        //}

        // Denne metoden viser detaljene for en brukerprofil hvis brukeren er logget inn
        public async Task<IActionResult> BrukerProfil()
        {
            // Hent innlogget bruker
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Hent innmeldinger for statistikk
            var innmeldinger = await _context.Innmelding
                .Where(i => i.BrukerID == currentUser.Id)
                .ToListAsync();

            // Beregn innmeldinger per måned for inneværende år
            var currentYear = DateTime.Now.Year;
            var submissionsPerMonth = new int[12];

            foreach (var innmelding in innmeldinger.Where(i => i.Dato.Year == currentYear))
            {
                submissionsPerMonth[innmelding.Dato.Month - 1]++;
            }

            // Hent unike år for innmeldinger
            var years = innmeldinger
                .Select(i => i.Dato.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();

            var viewModel = new BrukerProfilViewModel
            {
                Name = $"{currentUser.Fornavn} {currentUser.Etternavn}",
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                Fodselsdato = currentUser.Fodselsdato,
                SubmissionsPerMonth = new List<int> { 3, 2, 0, 3, 1, 0, 2, 1, 0, 4, 0, 3 },
                Years = new List<int> { 2022, 2023, 2024 }
            };

            return View(viewModel);
        }


    


        // IActionResult brukes her for å hente Views for forskjellige sider vi har i systemet
        [AllowAnonymous]
        public IActionResult Feilmelding()
        {
            return View();
        }

        public IActionResult Loading()
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

        [AllowAnonymous]
        public IActionResult KartInnmelding()
        {
            return View(new List<StedsnavnViewModel>());  // Tom Liste for Stedsnavn for å kunne søke etter Stedsnavn i kartavvik uten error ved første visning
        }


        // Håndterer søk etter Stedsnavn i kartinnmelding
        // Funker, ikke rør :)
        [AllowAnonymous]
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

        public IActionResult MineInnmeldinger()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult Hjelp()
        {
            return View();
        }

        public IActionResult Brukerinnmelding()
        {
            return View();
        }

        [AllowAnonymous]
         public IActionResult Registrert()
        {
            return View();
        }



        // H�ndterer s�k etter Kommuneinformasjon
        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult Sok()
        {
            return View();
        }

        // Handterer sok etter Stedsnavn
        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult KoordTilKommune()
        {
            return View();
        }



        [HttpGet]
        public IActionResult TestHedda()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAvvik(string Avvik)
        {
            try
            {
                if (string.IsNullOrEmpty(Avvik))
                {
                    ViewData["Error"] = "Vennligst fyll ut feltene.";
                    return View();
                }
                else
                {
                    var nyAvvik = new Avvikstype
                    {
                        Type = Avvik
                    };

                    _context.Avvikstype.Add(nyAvvik);
                    _context.SaveChanges();
                    return RedirectToAction("KartInnmelding");
                }
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View();
            }
        }



        [HttpGet]
        public IActionResult TestVetle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult lagStatus(Status status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    status = new Status
                    {
                        Statustype = status.Statustype,
                    };

                    _context.Status.Add(status);
                    _context.SaveChanges();
                    return RedirectToAction("TestVetle");

                }
                else
                {
                    return View(Feilmelding);
                }
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult lagAvvikstype(Avvikstype avvikstype)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    avvikstype = new Avvikstype
                    {
                        Type = avvikstype.Type,
                    };

                    _context.Avvikstype.Add(avvikstype);
                    _context.SaveChanges();
                    return RedirectToAction("TestVetle");

                }
                else
                {
                    return View(Feilmelding);
                }
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View();
            }
        }


        [HttpPost]
        public IActionResult lagPrioritet(Prioritet prioritet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    prioritet = new Prioritet
                    {
                        Prioritetsnivå = prioritet.Prioritetsnivå,
                    };

                    _context.Prioritet.Add(prioritet);
                    _context.SaveChanges();
                    return RedirectToAction("TestVetle");

                }
                else
                {
                    return View(Feilmelding);
                }
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> RedigerProfil()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var model = new RedigerBrukerViewModel
            {
                Fornavn = user.Fornavn,
                Etternavn = user.Etternavn,
                PhoneNumber = user.PhoneNumber,
                Fodselsdato = user.Fodselsdato,
                Profilbilde = user.Profilbilde
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RedigerProfil(RedigerBrukerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                user.Fornavn = model.Fornavn;
                user.Etternavn = model.Etternavn;
                user.PhoneNumber = model.PhoneNumber;
                user.Fodselsdato = model.Fodselsdato;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {

                    return RedirectToAction("BrukerProfil");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }




        // Laster inn tilfeldig bakgrunnsbilde fra wwwroot/Bakgrunnsbilder
        [AllowAnonymous]
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


        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}