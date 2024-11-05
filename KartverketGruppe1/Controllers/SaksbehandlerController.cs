using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartverketGruppe1.Controllers
{
    [Authorize(Roles = "Saksbehandler")]
    public class SaksbehandlerController : Controller
    {
        public IActionResult Behandling()
        {
            return View();
        }

        public IActionResult AlleHenvendelser()
        {
            return View();
        }

        public IActionResult Arkiv()
        {
            return View();
        }

        public IActionResult SaksbehandlerRediger()
        {
            return View();
        }
        
        public IActionResult Arbeidsbord()
        {
            return View();
        }

    }

    

   
}
