using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Status
    {
        [Key]
        public int Status_ID { get; set; }
        [Required]
        public string Statustype { get; set; }
    }
}
