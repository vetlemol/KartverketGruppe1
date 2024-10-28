using System;
using System.Collections.Generic;

namespace KartverketGruppe1.Models
{
    public class BrukerProfilViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public string GjentaPassword { get; set; }
        public List<int> SubmissionsPerMonth { get; set; }
        public List<int> Years { get; set; }
        public string ProfileImagePath { get; set; }
        
    }

}



