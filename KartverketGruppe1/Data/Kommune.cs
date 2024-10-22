using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Kommune
    {
        [Key]
        public int Kommune_ID { get; set; }
        [Required]
        public string Kommunenavn { get; set; }
        public int Kommunenummer { get; set; }
    }
}
