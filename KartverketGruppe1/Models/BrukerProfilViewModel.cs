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
        public List<int> SubmissionsPerMonth { get; set; }
        public List<int> Years { get; set; }

        

        public void OnGet()
        {
            // Initialiserer med eksempel
            Name = "Karsten Pettersen";
            Email = "eksempel@epost.com";
            Phone = "+ 47 12345678";
            BirthDate = new DateTime(1990, 1, 1);
            Password = "********"; // Placeholder for passord
                SubmissionsPerMonth = new List<int> { 3, 2, 0, 3, 1, 0, 2, 1, 0, 4, 0, 0 };
                Years = new List<int> { 2022, 2023, 2024 };
        }
    }

}



