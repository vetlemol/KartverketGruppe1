using KartverketGruppe1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KartverketGruppe1.Data
{
    public class Innmelding
    {

        [Key]
        public int InnmeldingID { get; set; }

        [Required(ErrorMessage = "Beskrivelse er påkrevd")]
        public string Innmeldingsbeskrivelse { get; set; }

        [Required]
        public DateTime Dato { get; set; }

        public byte[]? Dokumentasjon { get; set; }

        [Required]
        [ForeignKey("Kommune")]
        public int? KommuneID { get; set; }

        [Required]
        [ForeignKey("Avvikstype")]
        public int AvvikstypeID { get; set; }

        // Identity bruker-ID (string)
        [ForeignKey("Bruker")]
        public string? BrukerID { get; set; }

        [EmailAddress(ErrorMessage = "Ugyldig e-postformat")]
        public string? Gjest_epost { get; set; }

        [Required]
        [ForeignKey("Status")]
        public int StatusID { get; set; }

        [ForeignKey("Koordinat")]
        public int? KoordinatID { get; set; }

        [ForeignKey("Prioritet")]
        public int? PrioritetID { get; set; }

        // Identity saksbehandler-ID (string)
        [ForeignKey("Saksbehandler")]
        public string? SaksbehandlerID { get; set; }

        // Navigation properties
        public virtual Koordinat? Koordinat { get; set; }
        public virtual Prioritet? Prioritet { get; set; }
        public virtual Status Status { get; set; }
        public virtual Kommune? Kommune { get; set; }
        public virtual Avvikstype Avvikstype { get; set; }

        // Identity navigation properties
        public virtual ApplicationUser? Bruker { get; set; }
        public virtual ApplicationUser? Saksbehandler { get; set; }









        //public int InnmeldingID { get; set; }

        //[Required]
        //public string Innmeldingsbeskrivelse { get; set; }
        //[Required]
        //public DateTime Dato { get; set; }
        //public byte[]? Dokumentasjon { get; set; }
        //[Required]
        //public int? KommuneID { get; set; }
        //[Required]
        //public int AvvikstypeID { get; set; }
        //public string? BrukerID { get; set; }
        //public string? Gjest_epost { get; set; }
        //[Required]
        //public int StatusID { get; set; }
        //public int? KoordinatID { get; set; }
        //public int? PrioritetID { get; set; }
        //public string? SaksbehandlerID { get; set; }

        //public Koordinat? Koordinat { get; set; } 
        //public Prioritet Prioritet { get; set; } 
        //public Status Status { get; set; } 
        //// public Saksbehandler Saksbehandler { get; set; } 
        //// public Bruker Bruker { get; set; } 
        //public Kommune? Kommune { get; set; }
        //public Avvikstype Avvikstype { get; set; }

        //public virtual ApplicationUser? Bruker { get; set; }
        //public virtual ApplicationUser? Saksbehandler { get; set; }




    }
}
