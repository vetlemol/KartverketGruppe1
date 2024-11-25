using KartverketGruppe1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KartverketGruppe1.Data;
using System.Linq;

namespace KartverketGruppe1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
         /*Bruker dependency injection for å håndtere Identity-tjenester 
         * UserManager: Håndterer brukeroperasjoner mot databasen
         * SignInManager: Håndterer autentisering og sesjonsstyring
         */
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationDbContext _context;
        
        public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, 
        ILogger<AccountController> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous] // Tillater anonym tilgang
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (await _userManager.IsInRoleAsync(user, "Saksbehandler"))
                    {
                        return RedirectToAction("Arbeidsbord", "Saksbehandler");
                    }
                    return RedirectToAction("KartInnmelding", "Kart");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

          public IActionResult Loggut()
        {
            return View();
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(LagBrukerViewModel model)
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
                    await _userManager.AddToRoleAsync(user, "Bruker");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

       

    

     


        [HttpGet]
        public IActionResult SlettBruker()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SlettBrukerConfirmed()
        {
            var user = await _userManager.GetUserAsync(User); // Hent innlogget bruker
            if (user != null)
            {
                // Slett alle innmeldinger tilknyttet brukeren
                var innmeldinger = _context.Innmelding.Where(i => i.BrukerID == user.Id);
                _context.Innmelding.RemoveRange(innmeldinger);
                await _context.SaveChangesAsync();

                // Slett brukeren
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync(); // Logg ut brukeren
                    return RedirectToAction("SlettBruker", "Account"); // Send brukeren til slett-bruker-siden
                }
                // Legg til feil i ModelState for visning
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("SlettBruker"); // Vis slett-bruker-siden med feil
        }

        [HttpGet]
        public IActionResult RedigerProfil()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var model = new BrukerProfilViewModel
            {
                Name = user.Fornavn,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Fodselsdato = user.Fodselsdato ?? DateTime.MinValue,
                // Legg til andre nødvendige felter
            };
            return View(model);
        }
    }
}