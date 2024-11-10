using KartverketGruppe1.Data;
using KartverketGruppe1.Models;
using KartverketGruppe1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KartverketGruppe1.Controllers
{
    public class KartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IKommuneInfoService _kommuneInfoService;
        private readonly IStedsnavnService _stedsnavnService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public KartController(ILogger<HomeController> logger, IKommuneInfoService kommuneInfoService, IStedsnavnService stedsnavnService, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _kommuneInfoService = kommuneInfoService;
            _stedsnavnService = stedsnavnService;
        }
        private void LoadAvvikstyper()
        {
            ViewBag.Avvikstyper = _context.Avvikstype
                .Select(a => new SelectListItem
                {
                    Value = a.AvvikstypeID.ToString(),
                    Text = a.Type
                })
                .ToList();
        }

        [HttpGet]
        public IActionResult Innmelding()
        {
            LoadAvvikstyper();

            // return View();

            //public IActionResult FjernOversikt(int id)
            //{
            //    var bruker = _context.Bruker.Find(id); // Henter brukeren som er logget inn
            //    var innmeldinger = _context.Innmelding.Where(i => i.BrukerID == id).ToList(); // Henter alle innmeldinger brukeren har laget
            //    ViewBag.Bruker = bruker;
            //    ViewBag.Innmeldinger = innmeldinger;
            //    return View();
            //}

            var innmelding = new Innmelding
            {
                Dato = DateTime.Now,
                StatusID = 1,
                PrioritetID = 1
            };

            return View(innmelding);

        }


        [HttpPost]
        public async Task<IActionResult> Innmelding(Innmelding innmelding)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verifiser at koordinat og kommune eksisterer
                    var koordinat = await _context.Koordinat.FindAsync(innmelding.KoordinatID);
                    var kommune = await _context.Kommune.FindAsync(innmelding.KommuneID);

                    if (koordinat == null || kommune == null)
                    {
                        ModelState.AddModelError("", "Ugyldig koordinat eller kommune");
                        LoadAvvikstyper();
                        return View(innmelding);
                    }

                    innmelding.Dato = DateTime.Now;
                    innmelding.StatusID = 1;
                    innmelding.PrioritetID = 1;

                    // Håndter bruker-ID
                    if (User.Identity.IsAuthenticated)
                    {
                        innmelding.BrukerID = User.Identity.Name;
                    }

                    _context.Innmelding.Add(innmelding);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Registrert", "Home");
                }

                LoadAvvikstyper();
                return View(innmelding);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Feil ved lagring av innmelding");
                LoadAvvikstyper();
                ViewData["Error"] = e.Message;
                return View(innmelding);
            }
        }





          
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LagreKoordinatOgKommune([FromBody] KoordinatKommuneModel model)
        {
            try
            {
                // Logging for debugging
                _logger.LogInformation($"Mottok data: Lat={model.Latitude}, Lng={model.Longitude}, Kommune={model.Kommunenummer}");

                // Sjekk om kommunen finnes først
                var kommune = await _context.Kommune
                    .FirstOrDefaultAsync(k => k.Kommunenummer == model.Kommunenummer);

                // Opprett kommune hvis den ikke finnes
                if (kommune == null)
                {
                    kommune = new Kommune
                    {
                        Kommunenummer = model.Kommunenummer,
                        Kommunenavn = model.Kommunenavn
                    };
                    _context.Kommune.Add(kommune);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Opprettet ny kommune: {kommune.Kommunenavn}");
                }

                // Lagre koordinat
                var koordinat = new Koordinat
                {
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                };
                _context.Koordinat.Add(koordinat);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Lagret koordinat med ID: {koordinat.KoordinatID}");

                return Json(new
                {
                    koordinatId = koordinat.KoordinatID,
                    kommuneId = kommune.KommuneID
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Feil ved lagring av koordinat og kommune");
                return StatusCode(500, new { error = ex.Message });  // Returner feilmeldingen
            }
        }

        public IActionResult Registrert()
        {
            return View();
        }
    }

    // Helper model for koordinat og kommune data
    public class KoordinatKommuneModel
    {
        public string Koordinater { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Kommunenummer { get; set; }
        public string Kommunenavn { get; set; }
    }



}

