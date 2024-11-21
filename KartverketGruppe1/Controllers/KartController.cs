﻿using KartverketGruppe1.Data;
using KartverketGruppe1.Models;
using KartverketGruppe1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KartverketGruppe1.Controllers
{
    public class KartController : Controller
    {
        private readonly ILogger<KartController> _logger;
        private readonly IKommuneInfoService _kommuneInfoService;
        private readonly IStedsnavnService _stedsnavnService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public KartController(ILogger<KartController> logger, IKommuneInfoService kommuneInfoService, IStedsnavnService stedsnavnService, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _kommuneInfoService = kommuneInfoService;
            _stedsnavnService = stedsnavnService;
        }



        public IActionResult TestInnmelding()
        {
            LoadAvvikstyper();
            return View();
        }


        public IActionResult KartInnmelding()
        {
            LoadAvvikstyper();
            return View();
        }


        public IActionResult Poly()
        {
            LoadAvvikstyper();
            return View();
        }



        [HttpPost] // Denne som brukes i KartInnmelding.cshtml
        public async Task<IActionResult> LagreInnmelding(Innmelding innmelding)
        {
            try
            {
                // Sjekk at nødvendige relasjoner eksisterer
                var koordinat = await _context.Koordinat.FindAsync(innmelding.KoordinatID);
                var kommune = await _context.Kommune.FindAsync(innmelding.KommuneID);
                var avvikstype = await _context.Avvikstype.FindAsync(innmelding.AvvikstypeID);

                if (koordinat == null || kommune == null || avvikstype == null)
                {
                    ModelState.AddModelError("", "Ugyldig koordinat, kommune eller avvikstype");
                    LoadAvvikstyper();
                    return View(innmelding);
                }

                // Opprett ny innmelding
                var nyInnmelding = new Innmelding
                {
                    Innmeldingsbeskrivelse = innmelding.Innmeldingsbeskrivelse,
                    Dato = DateTime.Now,
                    KommuneID = innmelding.KommuneID,
                    AvvikstypeID = innmelding.AvvikstypeID,
                    KoordinatID = innmelding.KoordinatID,
                    StatusID = 1,
                    PrioritetID = 1
                };

                // Håndter bruker-ID
                if (User.Identity.IsAuthenticated)
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    nyInnmelding.BrukerID = currentUser?.Id;
                }
                else
                {
                    nyInnmelding.Gjest_epost = innmelding.Gjest_epost;
                }

                _context.Innmelding.Add(nyInnmelding);
                await _context.SaveChangesAsync();

                return RedirectToAction("Registrert", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Feil ved lagring av innmelding");
                LoadAvvikstyper();
                ViewData["Error"] = "Kunne ikke lagre innmeldingen. Prøv igjen senere.";
                return View(innmelding);
            }
        }











        [HttpPost]
        public async Task<IActionResult> TestInnmelding(Innmelding innmelding) // Funker!
        {
            try
            {
                // Sjekk at nødvendige relasjoner eksisterer
                var koordinat = await _context.Koordinat.FindAsync(innmelding.KoordinatID);
                var kommune = await _context.Kommune.FindAsync(innmelding.KommuneID);
                var avvikstype = await _context.Avvikstype.FindAsync(innmelding.AvvikstypeID);

                if (koordinat == null)
                    throw new Exception($"Fant ikke koordinat med ID {innmelding.KoordinatID}");
                if (kommune == null)
                    throw new Exception($"Fant ikke kommune med ID {innmelding.KommuneID}");
                if (avvikstype == null)
                    throw new Exception($"Fant ikke avvikstype med ID {innmelding.AvvikstypeID}");

                // Opprett ny innmelding
                var nyInnmelding = new Innmelding
                {
                    Innmeldingsbeskrivelse = innmelding.Innmeldingsbeskrivelse,
                    Dato = DateTime.Now,
                    KommuneID = innmelding.KommuneID,
                    AvvikstypeID = innmelding.AvvikstypeID,
                    KoordinatID = innmelding.KoordinatID,
                    StatusID = 1,
                    PrioritetID = 1
                };

                // Håndter bruker-ID korrekt
                if (User.Identity.IsAuthenticated)
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser != null)
                    {
                        nyInnmelding.BrukerID = currentUser.Id; // Bruk Id, ikke Name
                    }
                    else
                    {
                        // Hvis vi ikke finner brukeren, sett BrukerID til null
                        nyInnmelding.BrukerID = null;
                    }
                }
                else
                {
                    // For gjester
                    nyInnmelding.BrukerID = null;
                    nyInnmelding.Gjest_epost = innmelding.Gjest_epost;
                }

                _logger.LogInformation($"Lagrer innmelding med BrukerID: {nyInnmelding.BrukerID}, Gjest_epost: {nyInnmelding.Gjest_epost}");

                _context.Innmelding.Add(nyInnmelding);
                await _context.SaveChangesAsync();

                return RedirectToAction("Registrert", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Feil ved lagring av innmelding");
                LoadAvvikstyper();
                ViewData["Error"] = $"Feil ved lagring: {ex.Message}";
                return View(innmelding);
            }
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<IActionResult> SokStedsnavn(string? SokeTekst)
        //{
        //    if (string.IsNullOrEmpty(SokeTekst))
        //    {
        //        return View("KartInnmelding");
        //    }

        //    // Får fortsatt ArgumentNullException hvis den ikke finner noe på søketekst

        //    var stedsnavnResponse = await _stedsnavnService.GetStedsnavnAsync(SokeTekst);
        //    if (stedsnavnResponse?.Navn != null && stedsnavnResponse.Navn.Any())
        //    {
        //        var viewModel = stedsnavnResponse.Navn.Select(n => new StedsnavnViewModel
        //        {
        //            Nord = n.Representasjonspunkt.Nord,
        //            Ost = n.Representasjonspunkt.Ost
        //        }).ToList();

        //        return View("KartInnmelding", viewModel);
        //    }
        //    else
        //    {
        //        ViewData["Error"] = $"No results found for '{SokeTekst}'.";
        //        return View("KartInnmelding");
        //    }
        //}



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SokStedsnavn([FromBody] SokeRequest request)
        {
            if (string.IsNullOrEmpty(request?.SokeTekst))
            {
                return Json(new List<object>());
            }

            try
            {
                var stedsnavnResponse = await _stedsnavnService.GetStedsnavnAsync(request.SokeTekst);

                if (stedsnavnResponse?.Navn != null && stedsnavnResponse.Navn.Any())
                {
                    var resultater = stedsnavnResponse.Navn.Select(n => new
                    {
                        nord = n.Representasjonspunkt.Nord,
                        ost = n.Representasjonspunkt.Ost
                    }).ToList();

                    return Json(resultater);
                }

                return Json(new List<object>());
            }
            catch (Exception)
            {
                return Json(new List<object>());
            }
        }

        public class SokeRequest
        {
            public string SokeTekst { get; set; }
        }



        public async Task<IActionResult> Fullskjerm(int id)
        {
            //var currentUser = await _userManager.GetUserAsync(User);
            //if (currentUser == null)
            //{
            //    return NotFound();
            //}

            var innmelding = await _context.Innmelding
                .Include(i => i.Koordinat)
                .Include(i => i.Bruker)
                .Include(i => i.Kommune)
                .Include(i => i.Avvikstype)
                .Include(i => i.Status)
                .Include(i => i.Prioritet)
                .Include(i => i.Saksbehandler)
                .FirstOrDefaultAsync(i => i.InnmeldingID == id);

            if (innmelding == null)
            {
                return NotFound();
            }

            return View(innmelding);
        }


        public async Task<IActionResult> SeMeldinger(int id)
        {
            //var currentUser = await _userManager.GetUserAsync(User);
            //if (currentUser == null)
            //{
            //    return NotFound();
            //}

            var meldinger = await _context.Meldinger
                .Where(m => m.InnmeldingID == id)
                .OrderByDescending(m => m.SendeTidspunkt)
                .ToListAsync();

            if (meldinger == null)
            {
                return NotFound();
            }

            return View(meldinger);
        }




        // Lager en viewbag som henter verdiene fra avvikstyper-tabellen
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

           

            //var innmelding = new Innmelding
            //{
            //    //Dato = DateTime.Now,
            //    //StatusID = 3,
            //    //PrioritetID = 1
            //};

            return View(/*innmelding*/);

        }



        [HttpPost]
        // [ValidateAntiForgeryToken]

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









        [HttpGet]
        public IActionResult LagInnmelding()
        {
            LoadAvvikstyper();

            return View(new Innmelding());
        }



        [HttpPost]
        public async Task<IActionResult> LagInnmelding(Innmelding innmelding)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    // Verifiser at koordinat og kommune eksisterer
                                var koordinat = await _context.Koordinat.FindAsync(innmelding.KoordinatID);
                                var kommune = await _context.Kommune.FindAsync(innmelding.KommuneID);
                    LoadAvvikstyper();
                    if (User.Identity.IsAuthenticated)
                    {
                        innmelding.BrukerID = User.Identity.Name;
                        innmelding = new Innmelding
                        {
                            Innmeldingsbeskrivelse = innmelding.Innmeldingsbeskrivelse,
                            Dato = DateTime.Now,
                            StatusID = 3,
                            PrioritetID = 1,
                            BrukerID = innmelding.BrukerID,
                            Dokumentasjon = innmelding.Dokumentasjon,
                            KommuneID = innmelding.KommuneID,
                            AvvikstypeID = 1,
                            KoordinatID = innmelding.KoordinatID,
                            SaksbehandlerID = innmelding.SaksbehandlerID
                        };

                    }
                    else
                    {
                        innmelding = new Innmelding
                        {
                            Innmeldingsbeskrivelse = innmelding.Innmeldingsbeskrivelse,
                            Dato = DateTime.Now,
                            StatusID = 3,
                            PrioritetID = 1,
                            Dokumentasjon = innmelding.Dokumentasjon,
                            KommuneID = innmelding.KommuneID,
                            KoordinatID = innmelding.KoordinatID,
                            SaksbehandlerID = innmelding.SaksbehandlerID,
                            AvvikstypeID = 1,
                            Gjest_epost = innmelding.Gjest_epost
                        };
                    }
                    _context.Innmelding.Add(innmelding);
                    _context.SaveChanges();
                    return RedirectToAction("Home");

                }
                else
                {
                    LoadAvvikstyper();
                    return View(innmelding);
                }
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return View();
            }
        }



    }



    // Model for koordinat og kommune data
    public class KoordinatKommuneModel
    {
        public string Koordinater { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Kommunenummer { get; set; }
        public string Kommunenavn { get; set; }
    }













}

