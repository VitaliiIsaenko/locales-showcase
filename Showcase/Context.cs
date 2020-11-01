using Microsoft.EntityFrameworkCore;

namespace SessionsShowcase
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                @"Data Source=../../../sessions.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>()
                .ToTable("Session");
            modelBuilder.Entity<SessionTypeLocale>()
                .ToTable("SessionTypeLocale");

            modelBuilder.Entity<SessionTypeLocale>().HasKey(s => new {s.Id, s.Locale});

            modelBuilder.Entity<Session>()
                .HasMany(a => a.SessionTypeLocales)
                .WithOne()
                .HasForeignKey(x => x.Id)
                .HasPrincipalKey(x => x.TypeId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionTypeLocale> SessionTypeLocales { get; set; }
    }
}