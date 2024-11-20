using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Data
{
    public class Koordinat
    {
        [Key]
        public int KoordinatID { get; set; }
        public string? Koordinater { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? GeometryType { get; set; }
    }
}
