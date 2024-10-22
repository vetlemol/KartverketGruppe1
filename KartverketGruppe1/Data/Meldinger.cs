using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Meldinger
    {
        public int Melding_ID { get; set; }

        [Required]
        public string Innhold { get; set; }
        [Required]
        public DateTime Sende_Tidspunkt { get; set; }
        [Required]
        public int Innmelding_ID { get; set; }

        public Innmelding Innmelding { get; set; }
    }
}
