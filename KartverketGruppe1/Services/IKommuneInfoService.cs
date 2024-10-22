using KartverketGruppe1.APIModels;

namespace KartverketGruppe1.Services
{
    public interface IKommuneInfoService
    {
        Task<KommuneInfo> GetKommuneInfoAsync(string kommuneNr);
    }
}
