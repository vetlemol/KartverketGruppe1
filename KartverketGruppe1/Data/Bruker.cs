using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Bruker
    {
        public int BrukerID { get; set; }

        [Required]
        public string Fornavn { get; set; }
        [Required]
        public string Etternavn { get; set; }
        [Required]
        [EmailAddress]
        public string Epost { get; set; }
        [Required]
        public string Passord { get; set; }
        public string Telefonnummer { get; set; }
        public DateTime? Fodselsdato { get; set; }
        public byte[] Profilbilde { get; set; }
    }
}
