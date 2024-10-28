using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Saksbehandler
    {
        [Key]
        public int SaksbehandlerID { get; set; }
        [Required]
        public string Fornavn { get; set; }
        [Required]
        public string Etternavn { get; set; }
        [Required]
        [EmailAddress]
        public string Epost { get; set; }
        [Required]
        public string Ansvarsområde { get; set; }
        public string Avdeling { get; set; }
        [Required]
        public string Passord { get; set; }

        // Bilde for saksbehandler??
    }
}
