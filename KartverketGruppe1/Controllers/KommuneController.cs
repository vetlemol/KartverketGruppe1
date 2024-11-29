using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartverketGruppe1.Controllers
{
    public class KommuneController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Kommune/GetByCoordinate")]
        public async Task<IActionResult> GetByCoordinate(double lat, double lng, int koordsys = 4258) // Tar parametere fra midtpunkt av markering i kart
        {
            var baseUrl = _configuration["ApiSettings:KommuneInfoApiBaseUrl"]; // Henter URL fra appsettings.json
            var url = $"{baseUrl}/punkt?nord={lat}&ost={lng}&koordsys={koordsys}"; // Setter sammen URL for ekstern API

            try
            {
                var response = await _httpClient.GetAsync(url); // Henter data fra ekstern API 

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Content(content, "application/json"); // Returnerer data som JSON
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

         public KommuneController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

    }
}
