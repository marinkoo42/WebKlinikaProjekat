

using Microsoft.EntityFrameworkCore;

namespace Models{

    public class ClinicWebContext : DbContext{
        public DbSet<Odeljenje> Odeljenja {get;set;}
        public DbSet<Klinika> Klinike { get; set; }
        public DbSet<KlinikaOdeljenje> KlinikeOdeljenja { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }

        public DbSet<Grad> Gradovi {get;set;}

        public ClinicWebContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }
    }

}