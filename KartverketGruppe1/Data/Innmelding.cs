using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KartverketGruppe1.Data
{
    public class Innmelding
    {
        public int InnmeldingID { get; set; }

        [Required]
        public string Innmeldingsbeskrivelse { get; set; }
        public int? KommentarID { get; set; }
        [Required]
        public DateTime Dato { get; set; }
        public byte[] Dokumentasjon { get; set; }
        [Required]
        public int KommuneID { get; set; }
        [Required]
        public int AvvikstypeID { get; set; }
        public int? BrukerID { get; set; }
        public string? Gjest_epost { get; set; }
        [Required]
        public int StatusID { get; set; }
        [Required]
        public int? KoordinatID { get; set; }
        public int? PrioritetID { get; set; }
        public int? SaksbehandlerID { get; set; }

        public Kommentar Kommentar { get; set; } 
        public Koordinat Koordinat { get; set; } 
        public Prioritet Prioritet { get; set; } 
        public Status Status { get; set; } 
        public Saksbehandler Saksbehandler { get; set; } 
        public Bruker Bruker { get; set; } 
        public Kommune Kommune { get; set; }
        public Avvikstype Avvikstype { get; set; }


        
    }
}
