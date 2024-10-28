using Microsoft.EntityFrameworkCore;

namespace KartverketGruppe1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Saksbehandler> Saksbehandler { get; set; }
        public DbSet<Bruker> Bruker { get; set; }
        public DbSet<Kommune> Kommune { get; set; }
        public DbSet<Koordinat> koordinat { get; set; }
        public DbSet<Avvikstype> Avvikstype { get; set; }
        public DbSet<Innmelding> Innmelding { get; set; }
        public DbSet<Meldinger> Meldinger { get; set; }
        public DbSet<Prioritet> Prioritet { get; set; }
        public DbSet<Status> Status { get; set; }
        

    }
}
