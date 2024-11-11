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



        [HttpPost]
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


        //[HttpPost]
        //// [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Innmelding(Innmelding innmelding)
        //{
        //    try
        //    {
        //        if (innmelding.StatusID <= 0) innmelding.StatusID = 3;
        //        if (innmelding.PrioritetID <= 0) innmelding.PrioritetID = 1;
        //        // if (innmelding.Dato == null) innmelding.Dato = DateTime.Now;
        //        if (innmelding.AvvikstypeID <= 0) innmelding.AvvikstypeID = 1;

        //        if (ModelState.IsValid)
        //        {
        //            // Verifiser at koordinat og kommune eksisterer
        //            var koordinat = await _context.Koordinat.FindAsync(innmelding.KoordinatID);
        //            var kommune = await _context.Kommune.FindAsync(innmelding.KommuneID);

        //            if (koordinat == null || kommune == null)
        //            {
        //                ModelState.AddModelError("", "Ugyldig koordinat eller kommune");
        //                LoadAvvikstyper();
        //                return View(innmelding);
        //            }

        //            // Opprett ny innmelding med alle nødvendige felter
        //            var nyInnmelding = new Innmelding
        //            {
        //                Innmeldingsbeskrivelse = innmelding.Innmeldingsbeskrivelse,
        //                Dato = DateTime.Now,
        //                KommuneID = innmelding.KommuneID,
        //                AvvikstypeID = innmelding.AvvikstypeID,
        //                StatusID = 3,
        //                KoordinatID = innmelding.KoordinatID,
        //                PrioritetID = 1

        //            };

        //            // Håndter bruker-ID
                    

        //            // Legg til debug-logging
        //            _logger.LogInformation($"Forsøker å lagre innmelding: KoordinatID={nyInnmelding.KoordinatID}, KommuneID={nyInnmelding.KommuneID}");

        //            _context.Innmelding.Add(nyInnmelding);
        //            await _context.SaveChangesAsync();

        //            return RedirectToAction("Registrert", "Home");
        //        }

        //        // Hvis ModelState ikke er valid, logg feilene
        //        foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
        //        {
        //            _logger.LogError($"ModelState Error: {modelError.ErrorMessage}");
        //        }

        //        LoadAvvikstyper();
        //        return View(innmelding);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e, "Feil ved lagring av innmelding");
        //        LoadAvvikstyper();
        //        ViewData["Error"] = $"Feil ved lagring: {e.Message}";
        //        return View(innmelding);
        //    }
        //}





        //[HttpPost]
        //public async Task<IActionResult> Innmelding(Innmelding innmelding)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            // Verifiser at koordinat og kommune eksisterer
        //            var koordinat = await _context.Koordinat.FindAsync(innmelding.KoordinatID);
        //            var kommune = await _context.Kommune.FindAsync(innmelding.KommuneID);

        //            if (koordinat == null || kommune == null)
        //            {
        //                ModelState.AddModelError("", "Ugyldig koordinat eller kommune");
        //                LoadAvvikstyper();
        //                return View(innmelding);
        //            }

        //            innmelding.Dato = DateTime.Now;
        //            innmelding.StatusID = 1;
        //            innmelding.PrioritetID = 1;

        //            // Håndter bruker-ID
        //            if (User.Identity.IsAuthenticated)
        //            {
        //                innmelding.BrukerID = User.Identity.Name;
        //            }

        //            _context.Innmelding.Add(innmelding);
        //            await _context.SaveChangesAsync();

        //            return RedirectToAction("Registrert", "Home");
        //        }

        //        LoadAvvikstyper();
        //        return View(innmelding);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e, "Feil ved lagring av innmelding");
        //        LoadAvvikstyper();
        //        ViewData["Error"] = e.Message;
        //        Console.WriteLine(e.Message);
        //        return View(innmelding);
        //    }
        //}






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

