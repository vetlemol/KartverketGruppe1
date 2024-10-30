using KartverketGruppe1.APIModels;

namespace KartverketGruppe1.Services
{
    public interface IKommuneInfoService
    {
        Task<KommuneInfo> GetKommuneInfoAsync(string kommuneNr);

        // Ny metode
        //Task<KommuneInfo> GetKommuneByCoordinatesAsync(double lat, double lon);
    }
}
