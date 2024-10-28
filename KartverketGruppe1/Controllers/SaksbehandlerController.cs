using Microsoft.AspNetCore.Mvc;

namespace KartverketGruppe1.Controllers
{
    public class SaksbehandlerController : Controller
    {
        public IActionResult Behandling()
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
