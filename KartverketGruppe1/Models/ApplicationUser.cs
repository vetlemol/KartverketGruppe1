using KartverketGruppe1.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KartverketGruppe1.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Legg til feltene fra din eksisterende Bruker-tabell
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public DateTime? Fodselsdato { get; set; }
        public byte[]? Profilbilde { get; set; }

        public string? Område { get; set; }

        public virtual ICollection<Innmelding> Innmeldinger { get; set; }
    }
}