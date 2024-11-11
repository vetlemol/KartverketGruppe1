using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Kommune
    {
        [Key]
        public int KommuneID { get; set; }
        [Required]
        public string Kommunenavn { get; set; }
        public string Kommunenummer { get; set; }
    }
}
