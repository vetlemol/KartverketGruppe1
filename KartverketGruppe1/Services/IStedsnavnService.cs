using KartverketGruppe1.APIModels;

namespace KartverketGruppe1.Services
{
    public interface IStedsnavnService
    {
        Task<StedsnavnResponse> GetStedsnavnAsync(string search);
    }
}
