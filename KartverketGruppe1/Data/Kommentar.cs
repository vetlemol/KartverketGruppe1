using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Kommentar
    {
        
        public int KommentarID { get; set; }

        [Required]
        public string Beskrivelse { get; set; }
        [Required]
        public int InnmeldingID { get; set; }

        
        public Innmelding Innmelding { get; set; } // Foreign key
    }
}
