using KartverketGruppe1.Data;
using KartverketGruppe1.Models;
using KartverketGruppe1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KartverketGruppe1.Controllers
{
    [Authorize(Roles = "Saksbehandler")]
    // [AllowAnonymous]
    public class SaksbehandlerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<SaksbehandlerController> _logger;
        private readonly ApplicationDbContext _context;

        public SaksbehandlerController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<SaksbehandlerController> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }


        // [Authorize(Roles = "Saksbehandler")] // For at bare eksisterende saksbehandlere kan registrere nye, må se om vi skal ha dette.
        [AllowAnonymous]
        [HttpGet]
        public IActionResult SaksbehandlerRegistrer()
        {
            return View();
        }

        // [Authorize(Roles = "Saksbehandler")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registrer(RegistrerSaksbehandlerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Fornavn = model.Fornavn,
                    Etternavn = model.Etternavn
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Saksbehandler");
                    return RedirectToAction("Arbeidsbord");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }





        [Authorize(Roles = "Saksbehandler")]
        public IActionResult Behandling()
        {
            return View();
        }

        [Authorize(Roles = "Saksbehandler")]
        public IActionResult AlleHenvendelser()
        {
            return View();
        }

        [Authorize(Roles = "Saksbehandler")]
        public IActionResult MineHenvendelser()
        {
            return View();
        }

        [Authorize(Roles = "Saksbehandler")]
        public IActionResult Arkiv()
        {
            return View();
        }

        [Authorize(Roles = "Saksbehandler")]
        public IActionResult SaksbehandlerRediger()
        {
            return View();
        }

        [Authorize(Roles = "Saksbehandler")]
        public IActionResult Arbeidsbord()
        {
            return View();
        }



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
                //.Where(i => i.BrukerID == currentUser.Id)
                .OrderByDescending(i => i.Dato)
                .ToListAsync();

            return View(innmeldinger);
        }


        public async Task<IActionResult> Test(int id)
        {
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

            await LoadViewbags();
            return View(innmelding);
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

            // Hent alle brukere med rollen Saksbehandler
            var saksbehandlere = await _userManager.GetUsersInRoleAsync("Saksbehandler");
            ViewBag.Saksbehandler = saksbehandlere
                .Select(user => new SelectListItem
                {
                    Value = user.Id,
                    Text = user.UserName
                })
                .ToList();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OppdaterInnmelding(Innmelding innmelding)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Test), new { id = innmelding.InnmeldingID });
            }

            try
            {
                var eksisterendeInnmelding = await _context.Innmelding
                    .FindAsync(innmelding.InnmeldingID);

                if (eksisterendeInnmelding == null)
                {
                    return NotFound();
                }

                // Oppdater bare de feltene som kan endres
                eksisterendeInnmelding.StatusID = innmelding.StatusID;
                eksisterendeInnmelding.PrioritetID = innmelding.PrioritetID;
                eksisterendeInnmelding.SaksbehandlerID = innmelding.SaksbehandlerID;

                await _context.SaveChangesAsync();

                // Redirect tilbake til visningssiden
                return RedirectToAction(nameof(Test), new { id = innmelding.InnmeldingID });
            }
            catch (Exception ex)
            {
                // Logg feilen
                ModelState.AddModelError("", "Det oppstod en feil ved oppdatering av innmeldingen.");
                return RedirectToAction(nameof(Test), new { id = innmelding.InnmeldingID });
            }
        }




    }
}