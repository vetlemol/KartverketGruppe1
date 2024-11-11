using KartverketGruppe1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KartverketGruppe1.Controllers
{
    [Authorize(Roles = "Saksbehandler")]
    // [AllowAnonymous]
    public class SaksbehandlerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SaksbehandlerController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public IActionResult SaksbehandlerLogin()
        //{
        //    return View();
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);
        //        if (user != null && await _userManager.IsInRoleAsync(user, "Saksbehandler"))
        //        {
        //            var result = await _signInManager.PasswordSignInAsync(
        //                model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Arbeidsbord", "Saksbehandler");
        //            }
        //        }
        //        ModelState.AddModelError(string.Empty, "Ugyldig innlogging eller ikke saksbehandler.");
        //    }
        //    return View(model);
        //}

        // [Authorize(Roles = "Saksbehandler")] // For at bare eksisterende saksbehandlere kan registrere nye, må se om vi skal ha dette. Må uansett legge til en saksbehandler i databasen først.
        [HttpGet]
        [AllowAnonymous]
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

    }

    

   
}
