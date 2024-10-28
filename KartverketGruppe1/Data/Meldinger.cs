using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Meldinger
    {
        public int MeldingerID { get; set; }

        [Required]
        public string Innhold { get; set; }
        [Required]
        public DateTime SendeTidspunkt { get; set; }
        [Required]
        public int InnmeldingID { get; set; }

        public Innmelding Innmelding { get; set; }
    }
}
