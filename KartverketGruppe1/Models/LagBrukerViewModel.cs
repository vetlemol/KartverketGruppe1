using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Models
{
    public class LagBrukerViewModel
    {
        [Required]
        public string Fornavn { get; set; }

        [Required]
        public string Etternavn { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       
    }
}



//[Required(ErrorMessage = "Fornavn må fylles ut")] //Obligatoriske felt må fylles ut før de sendes
//public string Fornavn { get; set; }

//[Required(ErrorMessage = "Etternavn må fylles ut")]
//public string Etternavn { get; set; }

//[Required(ErrorMessage = "Epost må fylles ut")]
//[EmailAddress(ErrorMessage = "Epost må være en gyldig epostadresse")] //sjekker at det er i epost format (@)
//public string Epost { get; set; }

//[Required(ErrorMessage = "Passord må fylles ut")]
//[DataType(DataType.Password)] //Gjør at passordet blir skjult
//public string Passord { get; set; }
