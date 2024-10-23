namespace KartverketGruppe1.Models
{
    public class BrukerProfilViewModel
    {
        public UserProfile? UserProfile { get; set; }

        public void OnGet()
        {
            // Eksempeldata for brukerprofilen
            UserProfile = new UserProfile
            {
                Name = "Magnus Heggstad",
                Email = "magnusheggestad@eksempel.com",
                Phone = "+47 87 65 43 21",
                BirthDate = new DateTime(1973, 11, 5),
                Password = "********", // Placeholder for passord
                SubmissionsPerMonth = new List<int> { 3, 2, 0, 3, 1, 0, 2, 1, 0, 4, 0, 0 },
                Years = new List<int> { 2022, 2023, 2024 }
            };
        }
    }

    public class UserProfile
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public required string Password { get; set; }
        public required List<int> SubmissionsPerMonth { get; set; }
        public required List<int> Years { get; set; }
    }
}
