using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Koordinat
    {
        [Key]
        public int KoordinatID { get; set; }
        [Required]
        public string Koordinater { get; set; }
    }
}
