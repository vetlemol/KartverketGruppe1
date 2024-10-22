using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Avvikstype
    {
        public int AvvikstypeID { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
