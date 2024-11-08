using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Models
{
    public class RegistrerSaksbehandlerViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Fornavn { get; set; }

        [Required]
        public string Etternavn { get; set; }

        public string? Ansvarsområde { get; set; }
        public string? Avdeling { get; set; }
    }
}
