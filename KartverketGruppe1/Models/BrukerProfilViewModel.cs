using System;
using System.Collections.Generic;

namespace KartverketGruppe1.Models
{
    public class BrukerProfilViewModel
    {
        public string Name { get; set; }
        public DateTime? Fodselsdato { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[]? Profilbilde { get; set; }
        public string Password { get; set; }
        public string GjentaPassword { get; set; }
        public List<int> SubmissionsPerMonth { get; set; }
        public List<int> Years { get; set; }
        public string ProfileImagePath { get; set; }
        
    }

}



