using Microsoft.AspNetCore.Mvc;

namespace KartverketGruppe1.Controllers
{
    public class KommuneController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public KommuneController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("api/Kommune/GetByCoordinate")]
        public async Task<IActionResult> GetByCoordinate(double lat, double lng, int koordsys = 4258)
        {
            var baseUrl = _configuration["ApiSettings:KommuneInfoApiBaseUrl"];
            var url = $"{baseUrl}/punkt?nord={lat}&ost={lng}&koordsys={koordsys}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Content(content, "application/json");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
