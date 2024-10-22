using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Models
{
    public class LagBrukerViewModel
    {
        [Required(ErrorMessage = "Fornavn må fylles ut")] //Obligatoriske felt må fylles ut før de sendes
        public string Fornavn { get; set; }

        [Required(ErrorMessage = "Etternavn må fylles ut")]
        public string Etternavn { get; set; }

        [Required(ErrorMessage = "Epost må fylles ut")]
        [EmailAddress(ErrorMessage = "Epost må være en gyldig epostadresse")] //sjekker at det er i epost format (@)
        public string Epost { get; set; }

        [Required(ErrorMessage = "Passord må fylles ut")]
        [DataType(DataType.Password)] //Gjør at passordet blir skjult
        public string Passord { get; set; }

    }
}


