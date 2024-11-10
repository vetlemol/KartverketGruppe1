using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Models
{
    public class InnmeldingViewModel
    {
        public int? InnmeldingID { get; set; }

        public string? BrukerID { get; set; }

        public int KommuneID { get; set; }

        public int KoordinatID { get; set; }

        [Required(ErrorMessage = "Velg type avvik")]
        public int AvvikstypeID { get; set; }

        [Required(ErrorMessage = "Beskrivelse er påkrevd")]
        [MinLength(10, ErrorMessage = "Beskrivelsen må være minst 10 tegn")]
        public string Innmeldingsbeskrivelse { get; set; }

        public DateTime Dato { get; set; }

        public int Status { get; set; } = 1;

        public int Prioritet { get; set; } = 1;

        [EmailAddress(ErrorMessage = "Ugyldig e-postadresse")]
        public string? GjestEpost { get; set; }

        public string? GeometriType { get; set; }  // "Marker", "Polygon", eller "Line"

        public string? Koordinater { get; set; }  // JSON-string med geometridata

        public IFormFile? Bilde { get; set; }
    }

    // ViewModel for stedsøk
    public class StedsokViewModel
    {
        public int StedsnavnID { get; set; }
        public string Stedsnavn { get; set; }
        public double? Nord { get; set; }
        public double? Ost { get; set; }
        public int KommuneID { get; set; }
        public string KommuneNavn { get; set; }
    }

    // Kombinert ViewModel for siden
    public class KartInnmeldingPageViewModel
    {
        public InnmeldingViewModel Innmelding { get; set; }
        public List<StedsokViewModel> SokeResultater { get; set; }
        public string? SokeTekst { get; set; }

        public KartInnmeldingPageViewModel()
        {
            Innmelding = new InnmeldingViewModel();
            SokeResultater = new List<StedsokViewModel>();
        }
    }

}
