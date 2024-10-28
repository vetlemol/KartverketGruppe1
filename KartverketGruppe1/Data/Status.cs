using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }
        [Required]
        public string Statustype { get; set; }
    }
}
