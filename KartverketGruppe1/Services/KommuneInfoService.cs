using KartverketGruppe1.APIModels;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace KartverketGruppe1.Services
{
    public class KommuneInfoService : IKommuneInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<KommuneInfoService> _logger;
        private readonly ApiSettings _apiSettings;

        // Ny konstruktør
        private readonly string _baseUrl;

        // IConfiguration er lagt til for å kunne hente ut baseurl fra appsettings.json
        public KommuneInfoService(HttpClient httpClient, ILogger<KommuneInfoService> logger, IOptions<ApiSettings> apisettings, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiSettings = apisettings.Value;
            _baseUrl = configuration["ApiSettings:KommuneInfoApiBaseUrl"];
        }



        ////////////////////////////////////////////////
        //public async Task<KommuneInfo> GetKommuneByCoordinatesAsync(double lat, double lon)
        //{
        //    try
        //    {
        //        // Kartverket's API expects coordinates in EUREF89 UTM Zone 33N (EPSG:25833)
        //        // Convert WGS84 (lat/lon) to UTM
        //        var utmCoords = ConvertToUtm(lat, lon);

        //        var url = $"{_baseUrl}/punkt?nord={utmCoords.north}&ost={utmCoords.east}&koordsys=25833";
        //        var response = await _httpClient.GetAsync(url);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            return JsonSerializer.Deserialize<KommuneInfo>(content);
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error
        //        throw new Exception("Error fetching kommune info", ex);
        //    }
        //}
        //private (double north, double east) ConvertToUtm(double lat, double lon)
        //{
        //    // Using DotSpatial.Projections or ProjNet for coordinate conversion
        //    // This is a simplified example - you'll need to add proper conversion logic
        //    // You might want to use a proper coordinate conversion library

        //    // Placeholder conversion - replace with actual conversion logic
        //    return (lat * 111319.9, lon * 111319.9 * Math.Cos(lat * Math.PI / 180));
        //}

        ////////////////////////////////////////////////

        public async Task<KommuneInfo> GetKommuneInfoAsync(string kommuneNr)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiSettings.KommuneInfoApiBaseUrl}/kommuner/{kommuneNr}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"KommuneInfo Response: {json}");
                var kommuneInfo = JsonSerializer.Deserialize<KommuneInfo>(json);
                return kommuneInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching KommuneInfo for {kommuneNr}: {ex.Message}");
                return null;
            }

        }
    }
}
