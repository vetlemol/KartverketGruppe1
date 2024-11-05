using KartverketGruppe1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KartverketGruppe1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // Fjernet Bruker og Saksbehandler siden de nå håndteres av Identity
        public DbSet<Kommune> Kommune { get; set; }
        public DbSet<Koordinat> Koordinat { get; set; }
        public DbSet<Avvikstype> Avvikstype { get; set; }
        public DbSet<Innmelding> Innmelding { get; set; }
        public DbSet<Meldinger> Meldinger { get; set; }
        public DbSet<Prioritet> Prioritet { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Bruker-relasjon
            builder.Entity<Innmelding>()
                .HasOne<ApplicationUser>(i => i.Bruker)
                .WithMany(u => u.Innmeldinger)
                .HasForeignKey(i => i.BrukerID)
                .IsRequired(false);  // Siden BrukerID er nullable

            // Saksbehandler-relasjon
            builder.Entity<Innmelding>()
                .HasOne<ApplicationUser>(i => i.Saksbehandler)
                .WithMany()  // Ingen motsvarende collection i ApplicationUser
                .HasForeignKey(i => i.SaksbehandlerID)
                .IsRequired(false);
        }

    }
}
