using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Prioritet
    {
        [Key]
        public int Prioritet_ID { get; set; }
        [Required]
        public string Prioritetsnivå { get; set; }
    }
}
