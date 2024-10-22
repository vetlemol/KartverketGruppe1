using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Koordinat
    {
        [Key]
        public int Koordinat_ID { get; set; }
        [Required]
        public string Koordinater { get; set; }
    }
}
