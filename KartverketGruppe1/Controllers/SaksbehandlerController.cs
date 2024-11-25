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


        // [Authorize(Roles = "Saksbehandler")] // For at bare eksisterende saksbehandlere kan registrere nye.
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
                .Where(i => i.StatusID == 1 || i.StatusID == 2)
                .OrderByDescending(i => i.Dato)
                .ToListAsync();

            return View(innmeldinger);
        }

        public async Task<IActionResult> Arkivert()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if(currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var innmeldinger = await _context.Innmelding
                .Include(i => i.Status)
                .Include(i => i.Kommune)
                .Where(i => i.StatusID == 3 || i.StatusID == 4)
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

            // Hent alle brukere med rollen Saksbehandler
            var saksbehandlere = await _userManager.GetUsersInRoleAsync("Saksbehandler");
            ViewBag.Saksbehandler = saksbehandlere
                .Select(user => new SelectListItem
                {
                    Value = user.Id,
                    Text = user.UserName
                })
                .ToList();

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





        public async Task<IActionResult> RedigerInnmelding(int id)
        {
            var innmelding = await _context.Innmelding
                .Include(i => i.Status)
                .Include(i => i.Prioritet)
                .Include(i => i.Saksbehandler)
                .Include(i => i.Kommune)
                .Include(i => i.Avvikstype)
                .Include(i => i.Koordinat)
                .Include(i => i.Bruker)
                .FirstOrDefaultAsync(i => i.InnmeldingID == id);

            if (innmelding == null) return NotFound();

            await LoadViewbags(); // Trenger await for å kunne bruke async i metoden. Hvis ikke lastes ikke viewbags før viewen vises.
            return View(innmelding);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RedigerInnmelding(int id, Innmelding model)
        {
            if (id != model.InnmeldingID) return NotFound();

            try
            {
                var innmelding = await _context.Innmelding.FindAsync(id);
                innmelding.StatusID = model.StatusID;
                innmelding.PrioritetID = model.PrioritetID;
                innmelding.SaksbehandlerID = model.SaksbehandlerID;

                await _context.SaveChangesAsync();
                return RedirectToAction("Oversikt");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Feil ved oppdatering av innmelding");
                return View(model);
            }
        }



    }
}