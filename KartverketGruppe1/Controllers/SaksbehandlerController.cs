using Microsoft.AspNetCore.Mvc;

namespace KartverketGruppe1.Controllers
{
    public class SaksbehandlerController : Controller
    {
        public IActionResult Behandling()
        {
            return View();
        }
    }


}
