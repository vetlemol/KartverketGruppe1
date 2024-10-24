using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Kommentar
    {
        // Må se over, får mange til mange fra console når jeg prøver add-migration
        public int KommentarID { get; set; }

        [Required]
        public string Beskrivelse { get; set; }
        [Required]
        public int InnmeldingID { get; set; }

        
        public Innmelding Innmelding { get; set; } // Foreign key
    }
}
