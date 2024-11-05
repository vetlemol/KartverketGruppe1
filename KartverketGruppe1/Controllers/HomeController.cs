using KartverketGruppe1.Services;
using KartverketGruppe1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using KartverketGruppe1.Data;
using Microsoft.AspNetCore.Authorization;

namespace KartverketGruppe1.Controllers
{
    [Authorize(Roles = "Bruker")]
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Index(string Epost, string Passord) // Håndterer Parameterene fra innloggingsskjemaet i Index
        //{
        //    var user = _context.Bruker.SingleOrDefault(u => u.Epost == Epost && u.Passord == Passord);
        //    if (user != null)
        //    {
        //        HttpContext.Session.SetInt32("BrukerID", user.BrukerID);
        //        return RedirectToAction("FjernOversikt", new { id = user.BrukerID }); // Redirect til oversiktssiden for brukeren
        //    }
        //    else
        //    {
        //        ViewBag.Error = "Invalid email or password.";
        //        return View();
        //    }

        //}


        //public IActionResult FjernOversikt(int id)
        //{
        //    var bruker = _context.Bruker.Find(id); // Henter brukeren som er logget inn
        //    var innmeldinger = _context.Innmelding.Where(i => i.BrukerID == id).ToList(); // Henter alle innmeldinger brukeren har laget
        //    ViewBag.Bruker = bruker;
        //    ViewBag.Innmeldinger = innmeldinger;
        //    return View();
        //}

        public IActionResult FjernOversikt()
        {
            return View();
        }


        [HttpGet]
        public IActionResult LagBruker()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult LagBruker(Bruker bruker) 
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (bruker.Telefonnummer != null) // Sjekker om telefonnummer er fylt ut
        //            {
        //                bruker = new Bruker
        //                {
        //                    Fornavn = bruker.Fornavn,
        //                    Etternavn = bruker.Etternavn,
        //                    Epost = bruker.Epost,
        //                    Passord = bruker.Passord,
        //                    Telefonnummer = bruker.Telefonnummer,
        //                };

        //            }
        //            else
        //            {
        //                bruker = new Bruker // Trenger ikke ha telefonnummer her siden det er nullable
        //                {
        //                    Fornavn = bruker.Fornavn,
        //                    Etternavn = bruker.Etternavn,
        //                    Epost = bruker.Epost,
        //                    Passord = bruker.Passord
        //                };
        //            }

        //            _context.Bruker.Add(bruker);
        //            _context.SaveChanges();
        //            return RedirectToAction("BrukerProfil");

        //        }
        //        else
        //        {
        //            return View(Feilmelding);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ViewData["Error"] = e.Message;
        //        return View();
        //    }
        //}



        private static BrukerProfilViewModel _brukerProfil = new BrukerProfilViewModel
        {
            Name = "Ola Nordmann",
            Email = "eksempel@epost.com",
            Phone = "+47 12345678",
            BirthDate = new DateTime(1990, 1, 1),
            Password = "********",
            SubmissionsPerMonth = new List<int> { 3, 2, 0, 3, 1, 0, 2, 1, 0, 4, 0, 0 },
            Years = new List<int> { 2022, 2023, 2024 }
        };

        [HttpGet]
        public IActionResult BrukerProfil()
        {
            return View(_brukerProfil);
        }

        [HttpGet]
        public IActionResult RedigerBrukerProfil()
        {
            return View(_brukerProfil);
        }

        [HttpPost]
        public IActionResult RedigerBrukerProfil(BrukerProfilViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Oppdaterer brukerinformasjon
                _brukerProfil.Name = model.Name;
                _brukerProfil.Email = model.Email;
                _brukerProfil.Phone = model.Phone;
                _brukerProfil.BirthDate = model.BirthDate;
                _brukerProfil.Password = model.Password;


                return View(model); 
            }
            
            return RedirectToAction("BrukerProfil");
        }
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
        


        public IActionResult Hjelp()
        {
            return View();
        }

        public IActionResult Brukerinnmelding()
        {
            return View();
        }

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