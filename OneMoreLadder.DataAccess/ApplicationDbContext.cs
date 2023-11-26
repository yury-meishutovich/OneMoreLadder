using Microsoft.EntityFrameworkCore;
using OneMoreLadder.DataAccess.DataModel;

namespace OneMoreLadder.DataAccess
{
    internal  class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Player>().HasMany<Challenge>().WithOne(c => c.HomePlayer).HasPrincipalKey(p => p.PlayerId).HasForeignKey(c => c.HomePlayerId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Player>().HasMany<Challenge>().WithOne(c => c.GuestPlayer).HasPrincipalKey(p => p.PlayerId).HasForeignKey(c => c.GuestPlayerId).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Player>().HasMany<Match>().WithOne(c => c.HomePlayer).HasPrincipalKey(p => p.PlayerId).HasForeignKey(c => c.HomePlayerId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Player>().HasMany<Match>().WithOne(c => c.GuestPlayer).HasPrincipalKey(p => p.PlayerId).HasForeignKey(c => c.GuestPlayerId).OnDelete(DeleteBehavior.Restrict);
        }


        public DbSet<Player> Players { get; set; }

        public DbSet<Challenge> Challenges { get; set; }

        public DbSet<Match> Matches { get; set; }
    }
}
