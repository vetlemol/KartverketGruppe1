using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Prioritet
    {
        [Key]
        public int PrioritetID { get; set; }
        [Required]
        public string Prioritetsnivå { get; set; }
    }
}
