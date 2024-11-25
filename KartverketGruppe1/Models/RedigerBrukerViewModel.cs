namespace KartverketGruppe1.Models
{
    public class RedigerBrukerViewModel
    {
        public string? Fornavn { get; set; }
        public string? Etternavn { get; set; }
        public DateTime? Fodselsdato { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public byte[]? Profilbilde { get; set; }
    }
}
